GitStatistics
==========
In-development, noncomplete, library used for collecting Git repository statistics. Lots of breaking changes happening.

**Authors vs Committers**  
Right now it's assummed that the commit author == the committer. This could easily be separated into two, or taken as an option, whether to gather stats on the authors or committers.

**Committer email**  
Currently committers are identified solely by their email address. Commits by committers with no email address is not supported.