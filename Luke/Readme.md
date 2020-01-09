# Luke's Lab; Functional Software Design with C#

## tl;dr:

- Basic functional design is possible in C#
- Potential for more compact, more reusable and more readable code
- Object-oriented "boilerplate" code is not avoidable
- Delegates are usable but not ideal for function composition
- Strong types make functional design harder to realize

## Motivation

In this two day lab Samuel Bittmann and I dealt with the question of functional software design with C#.
The motivation is to have a better answer for this question with conrete code examples.
So we decided to come up with real scenarios to implement our approach of it.

## Functional Software Design

As simple as it sounds; functional design means to me that all design is made of functions - as all design in object-oriented design is made of objects (procedural design of procedures, etc.).
With that in mind my goal was to use nothing but functions to provide a usable and holistic design for a C# software.
To clarify what I mean with that; no layers, no objects, no processes, no nothing but functions.
In functional design you are only allowed to declare, pass, callback, transform, compose (etc.) and call functions. That's it, that's all.

## Scenario

I chose a common problem in all software; partially load data and aggregate it with additional data to get a result set.
In order to have an relating scenario I decided it to be the loading of porduct data, aggregating it with ratings and community data and transformation of the result.

## Approach

Yet before the lab I was "biased" with my knowledge of the mechanism C# provides for functions and function composition; delegate.
Thats why these became the first thing I implemented but I wanted to try out composition with Funcs as well, so I enriched the codebase with a Func version as well.

As you may notice everything is very basic and without much effort as I intented; to me no design is good that has much complexity and effort in it.
The solution is of course compromised because of using an object-oriented design language and many aspect seem to make little sense in terms of "practicality" - which isn't a requirement for a lab thou.

## Realization

### Part 1: Delegates

When I first learned about C# delegates I was excited; it reminded me much of the function composition capabilities of functional lagnuages I already knew and used.
I wondered why they haven't been used more often;.now I know why.

As many constructs in "non-functional" languages delegates are a step towards functional design, but only a step.
The biggest drawback is its "strong-typedness" - which isn't surprising in a strict type language - so that all functions implementing a delegate have to have the same "signature" (a delegate is like an interface for functions isntead of objects).

### Part 2: Funcs

Funcs let you declare functions, but to achieve a "composition" you have to implement it by yoursef, what I did simply with a list of funcs and means to call the funcs of that list in order (see PipelineFunctions class).
But there again; in composing functions together you are forced to have a matching return value over all functions in the composition (at least you don't implement laborious "binding-functions").

Thats why I decided to go with an "overall matching return type"; every function in the composition receives an object and gives back the same type (but not necessary and preferably the same object instance).
This way you don't have to worry about return types; it's drawback may be that this type has to be designed for the whole process (composition), which is not an issue to me because its the whole purpose of that type anyway.

### Part 3: Error Handling

"Error" is a difficult thing in functional terms; pure functional languages sometimes don't even know "errors" terminology, to them these are only "execution paths" (which they are in essence technically).
But I took it into my approach to ease critics; you can provide "error functions" in the pipeline which are triggered in case there went something "differently / not as expected" (look for "Safely" affix).

## Learnings & Conclusion

Functional software design is possible in C# but with rudimentary drawbacks in terms of effort and complexity (compared to functional languages).
The nature and origin of C# as a object-oriented language is of course not suitable for that and forced me several times to make unnecessary boilerplate code (static classes and static methods) and types ("binding type").
Nevertheless I managed to realize my desired approaches to a fully functional software design which is in my opinion more readable, more compact and more reusable.

## Comparison to Samuel's approach & solution

Samuel has a more "grounded" approach than me; he was guided by the existing setup and enriched it with functional "blessing" by injecting functions instead of objects into objects (controller) and (literally) "mapping" functions together with Chabis.Funcitonal functionalities.
This way he preserved all advantages of an object-oriented approach and adds functional programming to it (as Chabis.Functional already does).
On the other hand to me that isn't a fully functional design approach because of my definition above, although it has by itself many advantages over a pure object-oriented approach.
