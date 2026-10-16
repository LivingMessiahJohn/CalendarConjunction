
CREATE OR ALTER VIEW dbo.vw1Import
AS
/*
SELECT * FROM dbo.vw1Import ORDER BY i.Id
*/
SELECT
    i.Id,
    i.MonthHeading,
    d.Yom1Date,
    d.Yom1Note,
    d.Yom2Date,
    d.Yom2Note,
    d.Yom3Date,
    d.Yom3Note,
    d.Yom4Date,
    d.Yom4Note,
    d.Yom5Date,
    d.Yom5Note,
    d.Yom6Date,
    d.Yom6Note,
    d.Yom7Date,
    d.Yom7Note
FROM dbo.Import AS i
CROSS APPLY (
    SELECT
        MAX(CASE WHEN c.YomColumn = N'Yom1' THEN c.GregorianDate END) AS Yom1Date,
        MAX(CASE WHEN c.YomColumn = N'Yom1' THEN c.NoteText END) AS Yom1Note,
        MAX(CASE WHEN c.YomColumn = N'Yom2' THEN c.GregorianDate END) AS Yom2Date,
        MAX(CASE WHEN c.YomColumn = N'Yom2' THEN c.NoteText END) AS Yom2Note,
        MAX(CASE WHEN c.YomColumn = N'Yom3' THEN c.GregorianDate END) AS Yom3Date,
        MAX(CASE WHEN c.YomColumn = N'Yom3' THEN c.NoteText END) AS Yom3Note,
        MAX(CASE WHEN c.YomColumn = N'Yom4' THEN c.GregorianDate END) AS Yom4Date,
        MAX(CASE WHEN c.YomColumn = N'Yom4' THEN c.NoteText END) AS Yom4Note,
        MAX(CASE WHEN c.YomColumn = N'Yom5' THEN c.GregorianDate END) AS Yom5Date,
        MAX(CASE WHEN c.YomColumn = N'Yom5' THEN c.NoteText END) AS Yom5Note,
        MAX(CASE WHEN c.YomColumn = N'Yom6' THEN c.GregorianDate END) AS Yom6Date,
        MAX(CASE WHEN c.YomColumn = N'Yom6' THEN c.NoteText END) AS Yom6Note,
        MAX(CASE WHEN c.YomColumn = N'Yom7' THEN c.GregorianDate END) AS Yom7Date,
        MAX(CASE WHEN c.YomColumn = N'Yom7' THEN c.NoteText END) AS Yom7Note
    FROM (
        SELECT
            v.YomColumn,
            CAST(TRY_PARSE(
                CASE
                    WHEN v.Cell IS NULL THEN NULL
                    WHEN s3.Space3 = 0 THEN v.Cell
                    ELSE SUBSTRING(v.Cell, LEN(v.Cell) - s3.Space3 + 2, 40)
                END
            AS date USING 'en-US') AS date) AS GregorianDate,
            CASE
                WHEN v.Cell IS NULL OR s3.Space3 = 0 THEN NULL
                ELSE NULLIF(RTRIM(LEFT(v.Cell, LEN(v.Cell) - s3.Space3)), N'')
            END AS NoteText
        FROM (VALUES
            (N'Yom1', i.Yom1),
            (N'Yom2', i.Yom2),
            (N'Yom3', i.Yom3),
            (N'Yom4', i.Yom4),
            (N'Yom5', i.Yom5),
            (N'Yom6', i.Yom6),
            (N'Yom7', i.Yom7)
        ) AS v(YomColumn, Cell)
        CROSS APPLY (SELECT CHARINDEX(N' ', REVERSE(v.Cell)) AS Space1) AS s1
        CROSS APPLY (SELECT CHARINDEX(N' ', REVERSE(v.Cell), s1.Space1 + 1) AS Space2) AS s2
        CROSS APPLY (SELECT CHARINDEX(N' ', REVERSE(v.Cell), s2.Space2 + 1) AS Space3) AS s3
    ) AS c
) AS d;