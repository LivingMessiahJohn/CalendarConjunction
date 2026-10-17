/*

### Assumption
"For table Import the assumption is that every `Yom*` (Yom1-Yom7) will not be repeated"

Grok: 
> The assumption does not hold. 
> After the Id 45 change, 95 dates in `Import` still appear in two `Yom` cells. 
> None appears more than twice.

### month-boundary days smoke test
These repeats are the month-boundary days. 
The week that ends one month and the week that starts the next both contain the Rosh Kodesh evening and Yom 1. 
`1 September 2027` is no longer one of them.

query gotten by Grok
- `FlattenByDates` keeps one row per date, so it cannot show these repeats.
- This query reads `dbo.ImportByDays`, which keeps both copies, and uses MoonId.

SELECT TOP 20 
ImportId, GregorianDate, YomDay, YomNote, RoshKodeshDay, MoonId, MonthDescription
FROM dbo.ImportByDays 

*/
SELECT
RIGHT(MonthDescription, 4) AS Year
, FORMAT(CAST( GregorianDate AS DATE), 'MMM dd yyyy') AS Date
, COUNT(*) OVER (PARTITION BY CAST(GregorianDate AS DATE)) AS Dupes
, YomDay AS MonAge -- Day
--, RoshKodeshErev AS RKE, RoshKodeshDay AS RKD, MoonHalfWaxing AS Wax, MoonHalfWaning As Wan, MoonFull AS MF
, MoonId
, YomNote
FROM dbo.ImportByDays 
WHERE (YomNote IS NOT NULL ) 
  AND ( MoonId!=0)
--AND ( RoshKodeshErev=1 OR RoshKodeshDay=1 OR MoonFull = 1 OR MoonHalfWaning = 1 OR MoonHalfWaxing = 1)
ORDER BY CAST(GregorianDate AS DATE);  -- optional, groups the dupes together