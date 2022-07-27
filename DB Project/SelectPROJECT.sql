select * from SubCategory

select * from Product

select * from Category

select * from Country

select * from Brand

select * from Savor

select * from Size

select * from ProductDetail

select * from ProductSavor

select * from Account

select * from Role

select * from FavoriteProduct

select * from OrderDetail

select sum(quantity) as Quantity
from ProductSavor
where detailID = 1

select od.detailID, sum(od.quantity) as Best_Seller from OrderDetail od, ProductDetail pd, Product p, SubCategory s, Category c
where od.detailID = pd.detailID and pd.productID = p.productID and p.subCatID = s.subCatID and s.catID = c.catID and c.catID = 2
group by od.detailID
order by sum(od.quantity) desc


select sum(quantity) from OrderDetail
where detailID = 1
