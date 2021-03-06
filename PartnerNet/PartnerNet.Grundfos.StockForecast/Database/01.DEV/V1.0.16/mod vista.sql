ALTER VIEW [dbo].[ProductViewScala]
AS
SELECT     PRD.Id, SUM(STO.SC03003) AS CurrentStock, SUM(STO.SC03004) AS ReservedStock, SUM(STO.SC03006) AS OrderedStock, STOD.SC01055 AS Price, 
                      STOD.SC01056 AS Currency
FROM         Grundfos_Scala.dbo.SC030100 AS STO INNER JOIN
                      dbo.Product AS PRD ON PRD.ProductCode = STO.SC03001 INNER JOIN
                      Grundfos_Scala.dbo.SC010100 AS STOD ON STOD.SC01001 = STO.SC03001
WHERE     (STO.SC03002 IN ('01', '09'))
GROUP BY PRD.Id, STOD.SC01055, STOD.SC01056

