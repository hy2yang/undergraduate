select DualFrequency.*,AuthorIndex.cnt as scnt,
DualFrequency.cnt/AuthorIndex.cnt as 'p(1->12) 'into Rule1to12
from DualFrequency,AuthorIndex
where AuthorIndex.author=DualFrequency.author1
      and DualFrequency.cnt>2
	  and DualFrequency.cnt/AuthorIndex.cnt>0.7
