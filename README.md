# PaymentSystem
This project demonstarates the use of entityfromeworkcore in the form of a payment processing API .
The API consumes the following sample JSON body input: 
{
    "CreditCardNumber": "5338921946768966",
    "CardHolder": "Tma",
    "ExpirationDate": "2021-12-16T00:00:00",
    "SecurityCode": "073",
    "Amount": 501
}

and exposes is self at : serverName:port/api/makepayment.
Once supplied with all the relavant info the system will validate and process payment accordingly.


