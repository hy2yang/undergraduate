delete from dbo.SouceData
where 
( ID not in 
 (
  select max(ID) 
  from dbo.SouceData
  group by title,author
  )
 )
