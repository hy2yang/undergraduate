select SouceData.author,title,rank,ID into dbo.SingleAuthor
from dbo.SouceData,dbo.AuthorIndex
Where dbo.AuthorIndex.author=dbo.SouceData.author
order by author
