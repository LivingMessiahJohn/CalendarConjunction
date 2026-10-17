/*
    Reloads dbo.ImportByDays from dbo.vw1Import.

    This is the #Days logic from Create-FlattenByDates.sql.
    Every dated Yom cell is kept, including a month-boundary day that
    appears on both the week that ends one month and the week that starts
    the next. dbo.FlattenByDates is not changed.

    RoshKodeshDay is stored here. Create-FlattenByDates.sql uses it only
    to choose which duplicate Yom 1 to keep, then drops it.
*/
CREATE OR ALTER PROCEDURE [dbo].[stpPopulateImportByDays]
AS
BEGIN
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

    BEGIN TRANSACTION;

    DELETE FROM dbo.ImportByDays;

    INSERT INTO dbo.ImportByDays
    (
        ImportId,
        GregorianDate,
        YomDay,
        YomNote,
        RoshKodeshDay,
        MoonId,
        MonthDescription
    )
    SELECT
        ImportId,
        GregorianDate,
        YomDay,
        YomNote,
        RoshKodeshDay,
        MoonId,
        MonthDescription
    FROM #Days;

    -- The source file starts in Month Ten. 9 December 2026 is day 29,
    -- the Rosh Kodesh evening that ends Month Nine, and it has no earlier section.
    UPDATE dbo.ImportByDays
    SET MonthDescription = N'Month: Nine Year 5862'
    WHERE GregorianDate = '2026-12-09';

    COMMIT TRANSACTION;

    DROP TABLE #Days;

    SELECT
        COUNT(*) AS RowsInserted,
        COUNT(DISTINCT GregorianDate) AS DistinctDates,
        (
            SELECT COUNT(*)
            FROM
            (
                SELECT GregorianDate
                FROM dbo.ImportByDays
                GROUP BY GregorianDate
                HAVING COUNT(*) > 1
            ) AS repeated
        ) AS RepeatedDates
    FROM dbo.ImportByDays;
END;
GO
