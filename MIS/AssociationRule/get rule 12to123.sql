select TriFrequency.*,DualFrequency.cnt as scnt,
TriFrequency.cnt/DualFrequency.cnt as 'p(12->123) '
into Rule12to123
from TriFrequency,DualFrequency
where concat(DualFrequency.author1,DualFrequency.author2)=concat(TriFrequency.author1,TriFrequency.author2)
      and TriFrequency.cnt>2
	  and TriFrequency.cnt/DualFrequency.cnt>0.7