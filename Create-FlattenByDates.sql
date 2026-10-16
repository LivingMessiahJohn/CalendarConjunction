/*
    One row per populated day in dbo.vw1Import.

    YomDay:
      1. The note is only a number, e.g. 11.
      2. The leading number, e.g. 30 from '30Evening begins Rosh Kodesh'.
      3. 1 when the note starts with 'Yom 1 Month' or 'Yom Teruah'.

    YomNote keeps Yom*Note only when the note is more than those three shapes.
    A bare number, a Rosh Kodesh evening phrase, or 'Yom 1 Month {n} {year}'
    leaves YomNote NULL. The bit columns record Rosh Kodesh and the moon marks in YomNote.
    MoonHalfWaning is ◖, MoonHalfWaxing is ◗, and MoonFull is ○.
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

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
    CONSTRAINT [PK_FlattenByDates] PRIMARY KEY CLUSTERED ([GregorianDate] ASC, [ImportId] ASC),
    CONSTRAINT [CK_FlattenByDates_YomDay] CHECK ([YomDay] BETWEEN 1 AND 30)
);
GO

DECLARE @moon nvarchar(1) = NCHAR(9679);

DECLARE @halfWaning nvarchar(1) = NCHAR(9686); -- ◖
DECLARE @halfWaxing nvarchar(1) = NCHAR(9687); -- ◗
DECLARE @fullMoon nvarchar(1) = NCHAR(9675);   -- ○

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
    p.ImportId,
    p.GregorianDate,
    p.YomDay,
    p.YomNote,
    p.RoshKodeshErev,
    p.RoshKodeshDay,
    CONVERT(bit, CASE WHEN p.YomNote LIKE N'%' + @halfWaning + N'%' THEN 1 ELSE 0 END),
    CONVERT(bit, CASE WHEN p.YomNote LIKE N'%' + @halfWaxing + N'%' THEN 1 ELSE 0 END),
    CONVERT(bit, CASE WHEN p.YomNote LIKE N'%' + @fullMoon + N'%' THEN 1 ELSE 0 END),
    p.MonthDescription
FROM
(
    SELECT
        s.ImportId,
        s.GregorianDate,
        s.YomDay,
        CASE
            WHEN s.Note NOT LIKE N'%[^0-9]%' THEN NULL
            WHEN s.Rest = N'Evening begins Rosh Kodesh ' + @moon THEN NULL
            WHEN s.Note LIKE N'Yom 1 Month [0-9]% [0-9][0-9][0-9][0-9]'
             AND s.Note NOT LIKE N'Yom 1 Month [0-9]% [0-9][0-9][0-9][0-9] %' THEN NULL
            ELSE s.Note
        END AS YomNote,
        s.RoshKodeshErev,
        s.RoshKodeshDay,
        s.MonthDescription
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
        LTRIM(SUBSTRING(v.Note, NULLIF(PATINDEX(N'%[^0-9]%', v.Note), 0), 200)) AS Rest
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
) AS p
ORDER BY p.GregorianDate, p.ImportId;

SELECT
    COUNT(*) AS RowsInserted,
    SUM(CONVERT(int, MoonHalfWaning)) AS MoonHalfWaning,
    SUM(CONVERT(int, MoonHalfWaxing)) AS MoonHalfWaxing,
    SUM(CONVERT(int, MoonFull)) AS MoonFull
FROM dbo.FlattenByDates;
