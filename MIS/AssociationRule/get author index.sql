select SouceData.author,count(author) as cnt into dbo.AuthorIndex
from dbo.SouceData
group by author
having count(author)>1
