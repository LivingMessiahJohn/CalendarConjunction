/*
    One row per Gregorian date in dbo.vw1Import.

    The source repeats a month-boundary day on the week that ends one month
    and the week that starts the next. This script keeps that day once.
    Yom 1 stays with the later month. A day 29 or 30 Rosh Kodesh evening
    stays with the earlier month. When only one copy has extra note text,
    that copy wins.

    YomDay:
      1. The note is only a number, e.g. 11.
      2. The leading number, e.g. 30 from '30Evening begins Rosh Kodesh'.
      3. 1 when the note starts with 'Yom 1 Month' or 'Yom Teruah'.

    YomNote keeps text beyond those shapes.
    A bare number leaves YomNote NULL.
    'Evening begins Rosh Kodesh' and an optional ●, and 'Yom 1 Month {n} {year}',
    are removed even when more text follows. The extra text stays in YomNote.
    Example: '29 Evening begins Rosh Kodesh ●Omer Count 14' becomes 'Omer Count 14'.
    The bit columns record Rosh Kodesh and the moon marks from the original note.
    MoonHalfWaning is ◖, MoonHalfWaxing is ◗, and MoonFull is ○.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @moon nvarchar(1) = NCHAR(9679);       -- ●
DECLARE @halfWaning nvarchar(1) = NCHAR(9686); -- ◖
DECLARE @halfWaxing nvarchar(1) = NCHAR(9687); -- ◗
DECLARE @fullMoon nvarchar(1) = NCHAR(9675);   -- ○

IF OBJECT_ID(N'tempdb..#Days', N'U') IS NOT NULL
    DROP TABLE #Days;

SET ANSI_WARNINGS OFF;

SELECT
    s.ImportId,
    s.GregorianDate,
    s.YomDay,
    CASE
        WHEN s.Note NOT LIKE N'%[^0-9]%' THEN NULL
        WHEN s.Note LIKE N'%Evening begins Rosh Kodesh%' THEN
            NULLIF(LTRIM(SUBSTRING(s.Note, s.RoshKodeshTailAt, 200)), N'')
        WHEN s.Note LIKE N'Yom 1 Month [0-9]% [0-9][0-9][0-9][0-9]%' THEN
            NULLIF(LTRIM(SUBSTRING(s.Note, PATINDEX(N'%[0-9][0-9][0-9][0-9]%', s.Note) + 4, 200)), N'')
        ELSE s.Note
    END AS YomNote,
    s.RoshKodeshErev,
    s.RoshKodeshDay,
    s.MoonHalfWaning,
    s.MoonHalfWaxing,
    s.MoonFull,
    s.MonthDescription
INTO #Days
FROM
(
    SELECT
        i.Id AS ImportId,
        v.GregorianDate,
        v.Note,
        i.MonthHeading AS MonthDescription,
        CASE
            WHEN v.Note NOT LIKE N'%[^0-9]%' THEN TRY_CONVERT(tinyint, v.Note)
            WHEN v.Note LIKE N'Yom 1 Month %' OR v.Note LIKE N'Yom Teruah%' THEN CONVERT(tinyint, 1)
            WHEN PATINDEX(N'%[^0-9]%', v.Note) > 1 THEN TRY_CONVERT(tinyint, LEFT(v.Note, PATINDEX(N'%[^0-9]%', v.Note) - 1))
        END AS YomDay,
        CONVERT(bit, CASE WHEN v.Note LIKE N'%Evening begins Rosh Kodesh%' THEN 1 ELSE 0 END) AS RoshKodeshErev,
        CONVERT(bit, CASE WHEN v.Note LIKE N'Yom 1 Month %' OR v.Note LIKE N'Yom Teruah%' THEN 1 ELSE 0 END) AS RoshKodeshDay,
        CONVERT(bit, CASE WHEN v.Note LIKE N'%' + @halfWaning + N'%' THEN 1 ELSE 0 END) AS MoonHalfWaning,
        CONVERT(bit, CASE WHEN v.Note LIKE N'%' + @halfWaxing + N'%' THEN 1 ELSE 0 END) AS MoonHalfWaxing,
        CONVERT(bit, CASE WHEN v.Note LIKE N'%' + @fullMoon + N'%' THEN 1 ELSE 0 END) AS MoonFull,
        CASE
            WHEN CHARINDEX(@moon, v.Note) > 0 THEN CHARINDEX(@moon, v.Note) + 1
            ELSE CHARINDEX(N'Rosh Kodesh', v.Note) + LEN(N'Rosh Kodesh')
        END AS RoshKodeshTailAt
    FROM dbo.vw1Import AS i
    CROSS APPLY
    (
        VALUES
            (i.Yom1Date, i.Yom1Note),
            (i.Yom2Date, i.Yom2Note),
            (i.Yom3Date, i.Yom3Note),
            (i.Yom4Date, i.Yom4Note),
            (i.Yom5Date, i.Yom5Note),
            (i.Yom6Date, i.Yom6Note),
            (i.Yom7Date, i.Yom7Note)
    ) AS v (GregorianDate, Note)
    WHERE v.GregorianDate IS NOT NULL
) AS s;

SET ANSI_WARNINGS ON;

IF EXISTS
(
    SELECT 1
    FROM #Days AS a
    INNER JOIN #Days AS b
        ON a.GregorianDate = b.GregorianDate
       AND a.ImportId < b.ImportId
       AND (
            a.YomDay <> b.YomDay
            OR (a.YomNote IS NOT NULL AND b.YomNote IS NOT NULL AND a.YomNote <> b.YomNote)
       )
)
BEGIN
    SELECT
        a.ImportId,
        a.GregorianDate,
        a.YomDay,
        a.YomNote,
        a.MonthDescription
    FROM #Days AS a
    WHERE EXISTS
    (
        SELECT 1
        FROM #Days AS b
        WHERE b.GregorianDate = a.GregorianDate
          AND b.ImportId <> a.ImportId
          AND (
               a.YomDay <> b.YomDay
               OR (a.YomNote IS NOT NULL AND b.YomNote IS NOT NULL AND a.YomNote <> b.YomNote)
          )
    )
    ORDER BY a.GregorianDate, a.ImportId;

    THROW 50001, 'One Gregorian date has two different yom days or notes.', 1;
END;

BEGIN TRANSACTION;

IF OBJECT_ID(N'dbo.FlattenByDates', N'U') IS NOT NULL
    DROP TABLE dbo.FlattenByDates;

CREATE TABLE [dbo].[FlattenByDates]
(
    [ImportId]         INT            NOT NULL,
    [GregorianDate]    DATE           NOT NULL,
    [YomDay]           TINYINT        NOT NULL,
    [YomNote]          NVARCHAR (200) NULL,
    [RoshKodeshErev]   BIT            NOT NULL,
    [RoshKodeshDay]    BIT            NOT NULL,
    [MoonHalfWaning]   BIT            NOT NULL,
    [MoonHalfWaxing]   BIT            NOT NULL,
    [MoonFull]         BIT            NOT NULL,
    [MonthDescription] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_FlattenByDates] PRIMARY KEY CLUSTERED ([GregorianDate] ASC),
    CONSTRAINT [CK_FlattenByDates_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30)
);

INSERT INTO dbo.FlattenByDates
(
    ImportId,
    GregorianDate,
    YomDay,
    YomNote,
    RoshKodeshErev,
    RoshKodeshDay,
    MoonHalfWaning,
    MoonHalfWaxing,
    MoonFull,
    MonthDescription
)
SELECT
    d.ImportId,
    d.GregorianDate,
    d.YomDay,
    d.YomNote,
    d.RoshKodeshErev,
    d.RoshKodeshDay,
    d.MoonHalfWaning,
    d.MoonHalfWaxing,
    d.MoonFull,
    d.MonthDescription
FROM
(
    SELECT
        p.ImportId,
        p.GregorianDate,
        p.YomDay,
        p.YomNote,
        p.RoshKodeshErev,
        p.RoshKodeshDay,
        p.MoonHalfWaning,
        p.MoonHalfWaxing,
        p.MoonFull,
        p.MonthDescription,
        ROW_NUMBER() OVER
        (
            PARTITION BY p.GregorianDate
            ORDER BY
                CASE WHEN p.YomNote IS NULL THEN 1 ELSE 0 END,
                CASE WHEN p.RoshKodeshDay = 1 THEN -p.ImportId ELSE p.ImportId END,
                p.ImportId
        ) AS RowNum
    FROM #Days AS p
) AS d
WHERE d.RowNum = 1
ORDER BY d.GregorianDate;

COMMIT TRANSACTION;

DROP TABLE #Days;

SELECT
    COUNT(*) AS RowsInserted,
    COUNT(DISTINCT GregorianDate) AS DistinctDates,
    SUM(CONVERT(int, RoshKodeshErev)) AS RoshKodeshErev,
    SUM(CONVERT(int, RoshKodeshDay)) AS RoshKodeshDay,
    SUM(CONVERT(int, MoonHalfWaning)) AS MoonHalfWaning,
    SUM(CONVERT(int, MoonHalfWaxing)) AS MoonHalfWaxing,
    SUM(CONVERT(int, MoonFull)) AS MoonFull
FROM dbo.FlattenByDates;
