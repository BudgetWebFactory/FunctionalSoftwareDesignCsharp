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

## Scenario 1: GET feature toggles
I rebuilt the following functionality of the "get feature toggles" APi:

1. Retrieve the data for the requested feature toggle from a data source
2. Check, if the feature toggle is currently active for the provided user
3. Map the data to the response model
4. Return a 200 status code for existing feature toggles
5. Return a 404 (not found) status code if the requested feature toggle does not exist

## Scenario 2: POST shopping cart item
For the more complicated example I selected the "post shopping cart item" API and implemented the following functionality:

1. Validate user rights: Only logged in users can access this endpoint and users might only acces their own shopping cart
2. Validate the request itself: The requested shopping cart must have id 0 (since there is only one cart per user as of now) and
   the post body must contain a valid request
3. Load the current cart from a data source
4. Apeend the item from the request to the existing cart using product data from two additional data sources. If the requested
   item is already in the cart, only the quantity should be increased.
5. Persist the modified cart
6. Return the new cart state
7. Respond with an appropriate error status when necessary
