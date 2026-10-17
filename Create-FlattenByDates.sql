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
    The leading day number is removed because the Day column shows it.
    A moon mark is removed when MoonId already records that phase.
    'Yom {n} ChagHaMatzot {n}' and 'Yom {n} Sukkot {n}' keep the festival name.
    Separate messages are joined with a comma.
    Examples:
      '8 ◖' becomes NULL.
      '10 Lamb selection' becomes 'Lamb selection'.
      '15 Yom 1 ChagHaMatzot 1' becomes 'ChagHaMatzot 1'.
      '16 Yom 2 ChagHaMatzot 2 Omer Count 1' becomes 'ChagHaMatzot 2, Omer Count 1'.
      '2 Day 2/40 Repentance' becomes 'Day 2/40 Repentance'.
    A Rosh Kodesh evening is MoonId 1 (new). The next day is Yom 1 and has no
    moon of its own. Rosh Kodesh day is calculated only so that duplicated
    Yom 1 stays with the later month. It is not stored.
    MoonId matches CalendarApp.Enums.Moon:
      0 none, 1 new when the note is a Rosh Kodesh evening,
      2 waxing for ◖ near day 8, 3 full for ○, 4 waning for ◗ near day 22.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @moon nvarchar(1) = NCHAR(9679);        -- ●
DECLARE @waxingMark nvarchar(1) = NCHAR(9686); -- ◖ near day 8
DECLARE @waningMark nvarchar(1) = NCHAR(9687); -- ◗ near day 22
DECLARE @fullMark nvarchar(1) = NCHAR(9675);   -- ○

IF OBJECT_ID(N'tempdb..#Days', N'U') IS NOT NULL
    DROP TABLE #Days;

SET ANSI_WARNINGS OFF;

SELECT
    s.ImportId,
    s.GregorianDate,
    s.YomDay,
    NULLIF(c.Text, N'') AS YomNote,
    s.RoshKodeshDay,
    s.MoonId,
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
        CONVERT(bit, CASE WHEN v.Note LIKE N'Yom 1 Month %' OR v.Note LIKE N'Yom Teruah%' THEN 1 ELSE 0 END) AS RoshKodeshDay,
        CASE
            WHEN v.Note LIKE N'%' + @waxingMark + N'%' THEN 2
            WHEN v.Note LIKE N'%' + @waningMark + N'%' THEN 4
            WHEN v.Note LIKE N'%' + @fullMark + N'%' THEN 3
            WHEN v.Note LIKE N'%Evening begins Rosh Kodesh%' THEN 1
            ELSE 0
        END AS MoonId,
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
) AS s
CROSS APPLY
(
    SELECT
        CASE
            WHEN s.Note NOT LIKE N'%[^0-9]%' THEN NULL
            WHEN s.Note LIKE N'%Evening begins Rosh Kodesh%' THEN
                NULLIF(LTRIM(SUBSTRING(s.Note, s.RoshKodeshTailAt, 200)), N'')
            WHEN s.Note LIKE N'Yom 1 Month [0-9]% [0-9][0-9][0-9][0-9]%' THEN
                NULLIF(LTRIM(SUBSTRING(s.Note, PATINDEX(N'%[0-9][0-9][0-9][0-9]%', s.Note) + 4, 200)), N'')
            ELSE s.Note
        END AS Text
) AS b
CROSS APPLY
(
    SELECT
        CASE s.MoonId
            WHEN 1 THEN REPLACE(b.Text, @moon, N'')
            WHEN 2 THEN REPLACE(b.Text, @waxingMark, N'')
            WHEN 3 THEN REPLACE(b.Text, @fullMark, N'')
            WHEN 4 THEN REPLACE(b.Text, @waningMark, N'')
            ELSE b.Text
        END AS Text
) AS g
CROSS APPLY
(
    SELECT LTRIM(RTRIM(REPLACE(REPLACE(REPLACE(REPLACE(g.Text, N'  ', N' '), N'  ', N' '), N'  ', N' '), N'  ', N' '))) AS Text
) AS sp
CROSS APPLY
(
    SELECT CONVERT(nvarchar(3), s.YomDay) AS DayText
) AS dy
CROSS APPLY
(
    SELECT
        CASE
            WHEN sp.Text IS NULL OR sp.Text = N'' THEN NULL
            WHEN sp.Text = dy.DayText THEN NULL
            WHEN sp.Text LIKE dy.DayText + N' %' THEN LTRIM(SUBSTRING(sp.Text, LEN(dy.DayText) + 2, 200))
            ELSE sp.Text
        END AS Text
) AS d
CROSS APPLY
(
    SELECT PATINDEX(N'% Month [0-9]% [0-9][0-9][0-9][0-9]%', d.Text) AS At
) AS mp
CROSS APPLY
(
    SELECT
        CASE
            WHEN d.Text IS NULL OR mp.At = 0 THEN d.Text
            ELSE LTRIM(STUFF(
                d.Text,
                mp.At,
                PATINDEX(N'%[0-9][0-9][0-9][0-9]%', SUBSTRING(d.Text, mp.At, 40)) + 3,
                N''))
        END AS Text
) AS my
CROSS APPLY
(
    SELECT
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
        REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(REPLACE(
            my.Text,
            N'Yom 1 ChagHaMatzot 1', N'ChagHaMatzot 1'),
            N'Yom 2 ChagHaMatzot 2', N'ChagHaMatzot 2'),
            N'Yom 3 ChagHaMatzot 3', N'ChagHaMatzot 3'),
            N'Yom 4 ChagHaMatzot 4', N'ChagHaMatzot 4'),
            N'Yom 5 ChagHaMatzot 5', N'ChagHaMatzot 5'),
            N'Yom 6 ChagHaMatzot 6', N'ChagHaMatzot 6'),
            N'Yom 7 ChagHaMatzot 7', N'ChagHaMatzot 7'),
            N'Yom 8 ChagHaMatzot 8', N'ChagHaMatzot 8'),
            N'Yom 1 Sukkot 1', N'Sukkot 1'),
            N'Yom 2 Sukkot 2', N'Sukkot 2'),
            N'Yom 3 Sukkot 3', N'Sukkot 3'),
            N'Yom 4 Sukkot 4', N'Sukkot 4'),
            N'Yom 5 Sukkot 5', N'Sukkot 5'),
            N'Yom 6 Sukkot 6', N'Sukkot 6'),
            N'Yom 7 Sukkot 7', N'Sukkot 7'),
            N'Yom 8 Sukkot 8', N'Sukkot 8') AS Text
) AS f
CROSS APPLY
(
    SELECT LTRIM(RTRIM(
        REPLACE(REPLACE(REPLACE(
            f.Text,
            N'Pesach Evening begins', N'Pesach, Evening begins'),
            N' Omer Count ', N', Omer Count '),
            N' Day ', N', Day '))) AS Text
) AS c;

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
    [MoonId]           INT            NOT NULL,
    [MonthDescription] NVARCHAR (100) NOT NULL,
    CONSTRAINT [PK_FlattenByDates] PRIMARY KEY CLUSTERED ([GregorianDate] ASC),
    CONSTRAINT [CK_FlattenByDates_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30),
    CONSTRAINT [CK_FlattenByDates_MoonId] CHECK ([MoonId] BETWEEN 0 AND 4)
);

INSERT INTO dbo.FlattenByDates
(
    ImportId,
    GregorianDate,
    YomDay,
    YomNote,
    MoonId,
    MonthDescription
)
SELECT
    d.ImportId,
    d.GregorianDate,
    d.YomDay,
    d.YomNote,
    d.MoonId,
    d.MonthDescription
FROM
(
    SELECT
        p.ImportId,
        p.GregorianDate,
        p.YomDay,
        p.YomNote,
        p.MoonId,
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

-- The source file starts in Month Ten. 9 December 2026 is day 29,
-- the Rosh Kodesh evening that ends Month Nine, and it has no earlier section.
UPDATE dbo.FlattenByDates
SET MonthDescription = N'Month: Nine Year 5862'
WHERE GregorianDate = '2026-12-09';

COMMIT TRANSACTION;

DROP TABLE #Days;

EXEC sys.sp_executesql N'
SELECT
    COUNT(*) AS RowsInserted,
    COUNT(DISTINCT GregorianDate) AS DistinctDates,
    SUM(CASE WHEN MoonId = 0 THEN 1 ELSE 0 END) AS MoonNone,
    SUM(CASE WHEN MoonId = 1 THEN 1 ELSE 0 END) AS MoonNew,
    SUM(CASE WHEN MoonId = 2 THEN 1 ELSE 0 END) AS MoonWaxing,
    SUM(CASE WHEN MoonId = 3 THEN 1 ELSE 0 END) AS MoonFull,
    SUM(CASE WHEN MoonId = 4 THEN 1 ELSE 0 END) AS MoonWaning
FROM dbo.FlattenByDates;
';
