select  dbo.SingleAuthor.ID,dbo.SingleAuthor.title,dbo.SingleAuthor.author as author1,dbo.SingleAuthor.rank as rank1,
		dbo.SingleAuthor2.author as author2,dbo.SingleAuthor2.rank as rank2 into dbo.DualAuthors

from dbo.SingleAuthor,dbo.SingleAuthor2

where dbo.SingleAuthor.title=dbo.SingleAuthor2.title 
AND dbo.SingleAuthor.author<>dbo.SingleAuthor2.author 
AND dbo.SingleAuthor.rank<dbo.SingleAuthor2.rank
