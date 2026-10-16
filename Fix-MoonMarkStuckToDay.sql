/*
    Insert a space when a moon mark is stuck to the day number.
    Example: ○9 April 2028 -> ○ 9 April 2028
    Marks: ● U+25CF, ◖ U+25D6, ○ U+25CB, ◗ U+25D7
*/
SET NOCOUNT ON;
SET XACT_ABORT ON;

DECLARE @pattern nvarchar(20) =
    N'%[' + NCHAR(9679) + NCHAR(9686) + NCHAR(9675) + NCHAR(9687) + N'][0-9]%';

BEGIN TRANSACTION;

UPDATE dbo.Import
SET
    Yom1 = CASE WHEN PATINDEX(@pattern, Yom1) > 0 THEN STUFF(Yom1, PATINDEX(@pattern, Yom1) + 1, 0, N' ') ELSE Yom1 END,
    Yom2 = CASE WHEN PATINDEX(@pattern, Yom2) > 0 THEN STUFF(Yom2, PATINDEX(@pattern, Yom2) + 1, 0, N' ') ELSE Yom2 END,
    Yom3 = CASE WHEN PATINDEX(@pattern, Yom3) > 0 THEN STUFF(Yom3, PATINDEX(@pattern, Yom3) + 1, 0, N' ') ELSE Yom3 END,
    Yom4 = CASE WHEN PATINDEX(@pattern, Yom4) > 0 THEN STUFF(Yom4, PATINDEX(@pattern, Yom4) + 1, 0, N' ') ELSE Yom4 END,
    Yom5 = CASE WHEN PATINDEX(@pattern, Yom5) > 0 THEN STUFF(Yom5, PATINDEX(@pattern, Yom5) + 1, 0, N' ') ELSE Yom5 END,
    Yom6 = CASE WHEN PATINDEX(@pattern, Yom6) > 0 THEN STUFF(Yom6, PATINDEX(@pattern, Yom6) + 1, 0, N' ') ELSE Yom6 END,
    Yom7 = CASE WHEN PATINDEX(@pattern, Yom7) > 0 THEN STUFF(Yom7, PATINDEX(@pattern, Yom7) + 1, 0, N' ') ELSE Yom7 END
WHERE PATINDEX(@pattern, Yom1) > 0
   OR PATINDEX(@pattern, Yom2) > 0
   OR PATINDEX(@pattern, Yom3) > 0
   OR PATINDEX(@pattern, Yom4) > 0
   OR PATINDEX(@pattern, Yom5) > 0
   OR PATINDEX(@pattern, Yom6) > 0
   OR PATINDEX(@pattern, Yom7) > 0;

IF EXISTS (
    SELECT 1
    FROM dbo.Import
    WHERE PATINDEX(@pattern, Yom1) > 0
       OR PATINDEX(@pattern, Yom2) > 0
       OR PATINDEX(@pattern, Yom3) > 0
       OR PATINDEX(@pattern, Yom4) > 0
       OR PATINDEX(@pattern, Yom5) > 0
       OR PATINDEX(@pattern, Yom6) > 0
       OR PATINDEX(@pattern, Yom7) > 0
)
BEGIN
    ROLLBACK TRANSACTION;
    THROW 50000, 'A moon mark is still stuck to a day.', 1;
END;

COMMIT TRANSACTION;

SELECT Id, Yom1, Yom4
FROM dbo.Import
WHERE Id IN (23, 83)
ORDER BY Id;
