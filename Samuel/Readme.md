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

## Learnings

0. Thinking in small, self-contained functions seemed to favour the reusability of the code. When I added a GET endpoint for the shopping cart API after having implemented the POST endpoint for cart items I could re-compose the existing functions without needing to write new ones. It would be interesting to see, how to handling a rising need for performance optimization.
0. For this lab I heavily relied on the existing Chabis.Functional library. This allowed me to write well structured and easy to read code quickly. Common complexities are abtracted in the Option and Result classes. In my experience, whithout this complexity the self-written code tends to be much simpler and therefor better suited for reusing it.
0. Testing the code was super easy. I avoided state where possible and made as many methods pure. This gave me great flexibilty when writing the test.
0. I strictly separated functions contianing business logic from mapper functions in the "UI layer". For instance, the `AddItem(...)` function could have returned `Result<Cart, HttpStatusCode>` directly. Because in my scenario this operation cannot fail I opted to always just return a cart and map the response in the controller instead. This makes it possible to use the function in other places without having to deal with the `HttpStatusCode`.
0. Although I clearly see its benefits the mapping in C# was still tedious. Unfortunately there is no spread operator like in JavaScript and the type system requires the programmer to specify almost everything explicitly. Switching to F# for example would have made this significantly easier.
0. Despite the promises made by Mark Seeman not everything comes automatically when using the functional paradigm. You still need to make sure to highlight important business rules in your code instead of hiding it. However, i found it much easier to do so compared to OOP.

## Conclusion
In my oppinion it can be highly beneficial to borrow concepts from the funcitonal world even when working in C#. Immutability and the idea of pure functions tend to make the code much easier to understand and therefor easier to expand and to fix. The benefit of increased testability should also lead to more stable code. (>>>> What about separating data and functions? Object with immutable (private) fields and instance methods? <<<<<). If you are free from constraints and decide to use the functional paradigm fully in a project, I would recommend to look into using a functional programming language instead.
