# FinalProject

•	What are the different endpoints that a client can use? 
        
        GET
        
        GET api/Reservation
        GET api/Resturant 
        GET api/Reservation {id}
        GET api/Resturant {id}


        POST

        GET api/Reservation
        GET api/Resturant 

        Delete 

        Delete api/Reservation {id}
        Delete api/Resturant {id}

        Put
        
        Put api/Reservation{id}
        Put api/Resturant {id}


•	Sample request bodies, if applicable?

    {
    "statusCode": 200,
    "statusDescription": "Successfully",
    "reservations": [
        {
            "reservationId": 1,
            "restaurantId": 2,
            "reservationTime": "2023-03-08T13:45:30",
            "customerName": "Max",
            "size": 4
        },
        {
            "reservationId": 2,
            "restaurantId": 1,
            "reservationTime": "2023-03-08T20:45:30",
            "customerName": "Bob",
            "size": 5
        },
        {
            "reservationId": 4,
            "restaurantId": 3,
            "reservationTime": "2023-04-08T20:00:00",
            "customerName": "Sun",
            "size": 10
        }
    ],
    "restaurants": []
}

•	Sample response body? (There should only be one, as this should be consistent. More information on this further below). 


    PUT https://localhost:7173/api/Restaurants/1

    {
    "restaurantID": 1,
    "name": "GOOD PLACE",
    "rating": 10.0,
    "phoneNumber": "666-666-6666"
    }