# OperatingSystem-Garbage-collection

![alt text]https://github.com/samuelbenichou/OperatingSystem-Garbage-collection/blob/master/unnamed.png)

![Build Status](https://travis-ci.org/lemire/JavaFastPFOR.png)
![Price](https://img.shields.io/badge/price-FREE-0098f7.svg)

The garbage collector, attempts to reclaim garbage, or memory occupied by objects that are no longer in use by the program.

## Mark and Sweep Algorithm
Any garbage collection algorithm must perform 2 basic operations. One, it should be able to detect all the unreachable objects and secondly, it must reclaim the heap space used by the garbage objects and make the space available again to the program.
The above operations are performed by Mark and Sweep Algorithm in two phases:
- Mark phase
- Sweep phase
