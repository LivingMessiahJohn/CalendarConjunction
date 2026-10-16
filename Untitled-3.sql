
SELECT TOP 500 *
FROM FlattenByDates 
ORDER BY GregorianDate, YomDay

-- 1	2026-12-09	29	NULL	1	0	Month: Ten Year 5862
SELECT 
--TOP 200  
ImportId, DATEPART(week, GregorianDate) AS Week, GregorianDate, YomDay, YomNote, RoshKodeshErev, RoshKodeshDay	MonthDescription
FROM FlattenByDates 
WHERE YomNote IS NOT NULL
-- WHERE YomDay = 21
ORDER BY GregorianDate, YomDay
--ORDER BY ImportId

SELECT  DISTINCT YomNote
FROM FlattenByDates 
WHERE YomNote IS NOT NULL

SELECT  DISTINCT YomNote
FROM FlattenByDates 
WHERE YomNote IS NOT NULL AND MoonHalfWaning=0 AND MoonHalfWaxing=0 AND MoonFull=0

