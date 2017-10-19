select TriFrequency.*,DualFrequency.cnt as scnt,
TriFrequency.cnt/DualFrequency.cnt as 'p(23->123) '
into Rule23to123
from TriFrequency,DualFrequency
where concat(DualFrequency.author1,DualFrequency.author2)=concat(TriFrequency.author2,TriFrequency.author3)
      and TriFrequency.cnt>2
	  and TriFrequency.cnt/DualFrequency.cnt>0.7
