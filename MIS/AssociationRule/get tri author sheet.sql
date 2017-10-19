select  A.ID,A.title,
        A.author as author1,A.rank as rank1,
		B.author as author2,B.rank as rank2,
		C.author as author3,C.rank as rank3
into TriAuthors
from SingleAuthor A,SingleAuthor B,SingleAuthor C

where A.title=B.title and B.title=C.title
AND A.author<>B.author and B.author<>C.author and C.author<>A.author 
AND A.rank<B.rank and B.rank<C.rank
