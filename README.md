# PROMOTIONUNIT

Promotion Unit is a console application that applies currently active promotions on a given shopping cart

# ASSUMPTIONS
* Only one promotion per product
* On the shopping cart , one entry per product, and total count of the product
* Unit price given for all the products

# TESTDATA

The class DemoDataManager contains various functions to add sample data like
 * Unit price for each product
 * Sample shopping cart
 * Multibuy promotions (eg. 3 A for 120)
 * Duocombo promotions (eg. C and D for 45)
 
 Right now ,the sample data in incorperated in the code. It can be modified to accept data from the console or from other files or other business logic units.
 We can add multiple promotions for each type, with different combinations. But only one promotion per product at the moment.


# UNITTEST

Contains various tests for each of the functions also inline data
