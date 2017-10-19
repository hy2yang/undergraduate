select DualFrequency.*,AuthorIndex.cnt as scnt,
DualFrequency.cnt/AuthorIndex.cnt as 'p(2->12) ' into Rule2to12
from DualFrequency,AuthorIndex
where AuthorIndex.author=DualFrequency.author2
      and DualFrequency.cnt>2
	  and DualFrequency.cnt/AuthorIndex.cnt>0.7
