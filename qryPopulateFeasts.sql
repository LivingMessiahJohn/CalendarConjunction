/*
PK: GregorianDate

SELECT MIN(GregorianDate) AS FirstDate, MAX(GregorianDate) AS LastDate -- 2026-12-09 | 2031-05-23
, DATEDIFF(day, MIN(GregorianDate), MAX(GregorianDate)) AS TotalDays   -- 1,624 + 1 = Rows(1,625)
, COUNT(*) AS Rows                                                     -- 1,625
FROM FlattenByDates 

DECLARE @start int = CHARINDEX('ChagHaMatzot', @str);
DECLARE @comma int = CHARINDEX(',', @str, @start);

*/

DECLARE @feast varchar(25) = 'ChagHaMatzot';

SELECT 
GregorianDate, YomNote
, 'INSERT INTO Feast (GregorianDate, Feast) VALUES(''' 
        + CONVERT(varchar(10), GregorianDate, 120) 
        + ''', ''' 
        + CASE 
            WHEN CHARINDEX(',', YomNote, CHARINDEX(@feast, YomNote)) > 0
            THEN SUBSTRING(
                    YomNote,
                    CHARINDEX(@feast, YomNote),
                    CHARINDEX(',', YomNote, CHARINDEX(@feast, YomNote)) 
                        - CHARINDEX(@feast, YomNote)
                 )
            ELSE SUBSTRING(
                    YomNote,
                    CHARINDEX(@feast, YomNote),
                    LEN(YomNote)
                 )
          END
        + ''');' AS SqlInsert
/*
*/
FROM FlattenByDates
WHERE YomNote IS NOT NULL AND YomNote LIKE '%ChagHaMatzot%'
ORDER BY GregorianDate

/*

*/

