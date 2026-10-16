--select * from Import

-- SELECT * FROM dbo.vw1Import ORDER BY Id

SELECT Id
, MonthHeading
--, Yom1Date, Yom2Date, Yom3Date, Yom4Date, Yom5Date, Yom6Date, Yom7Date
, Yom1Note, Yom2Note, Yom3Note, Yom4Note, Yom5Note, Yom6Note, Yom7Note
FROM dbo.vw1Import 
ORDER BY Id
