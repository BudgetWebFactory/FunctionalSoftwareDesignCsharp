# Part 2: Samuels lab

## My approach
My goal was to highlight key benefits and disadvantages of software design used in both Object Oriented Programming (OOP) and 
Functional Programming (FP). I aimed to find advantages and disadvantages in both approaches. In order to see and understand these
differences I wanted to (at least partly) rebuild a rather well known functionality in the existing devinite code base. I chose to
imitate two rest APIs: The "get featuretoggle" API and the "post shopping cart item" API. While the first API turned out to be a
perfect starting point due to its simplicity the second one comes with a lot more requirements and therefore challenges to code
design. Unfortunately I was not able to build an exact replica of both APIs due to the time limit. However, in my oppinion the
selected feature set for both endpoints represent challenging real world examples which allow for comparing the production code
written with OOP in mind with my FP approach.

When shaping my design for the code I especially regarded the following principles:

1. Pure functions, i.e. funcitons that only depend on its input variables and do not trigger any side effects, should be maximized.
   This leads to code that is easier to understand and to test, as Mark Seeman pointed out in his talk [?].
2. Mutable state should be avoided fully since it can quickly lead to convoluted, and thus, overly complex code.
3. Data and functions are to be strictly separated. This, of course, is a core principle of functional programming.
