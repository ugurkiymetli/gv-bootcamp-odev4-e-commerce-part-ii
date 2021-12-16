<!-- Created by UgurKiymetli -->

# E-Commerce Project with .Net 5.

Simple E-Commerce WebApi project.

## **Frontend UI**

I made a simple UI for this project with React <3. You can check it out **[here](https://github.com/ugurkiymetli/emerce-frontend)**.

## Features

- User Register,Login
- Create, delete, update (User, Product, Category)
- User must be logged in in order to view, create, update or delete items.
- Products can be queried in pages. Pagination can be done with user inputs (pageSize, pageNumber).

# API Reference

<sub>keep in mind that _https://localhost:44359_ can change for you.<sub>

<details>
<summary>Week 3 -  Assignment</summary>

## Categories

## Create a new category

### Request

`POST /Category/`

```curl
curl --location --request POST 'https://localhost:44359/api/Category' \
     --header 'Content-Type: application/json' \
     --data-raw
     '{
          "name": "category1",
          "description": "category1",
          "iuser": 1
     }'
```

### Response

```json
{
  "isSuccess": true,
  "entity": {
    "name": "category1",
    "description": "category1",
    "idatetime": "2021-12-07T22:51:00.7407267+03:00",
    "iuser": 1
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Get list of categories

### Request

`GET /api/Category`

```curl
curl --location --request GET 'https://localhost:44359/api/Category'
```

### Response

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "name": "category1",
      "description": "category1",
      "idatetime": "2021-12-07T22:51:00.7407267+03:00",
      "iuser": "user1"
    },
    {
      "id": 2,
      "name": "category2",
      "description": "category2",
      "idatetime": "2021-12-07T22:51:30.3207267+03:00",
      "iuser": "user1"
    }
  ],
  "totalCount": 2,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Update a Category

`PUT /Category/id`

### Request

```
curl --location
    --request PUT 'https://localhost:44359/api/Category/1' \
    --header 'Content-Type: application/json' \
    --data-raw
    '{
        "name": "category1",
        "description": "category1Test",
        "uuser": 1
    }'
```

### Response

```json
{
  "isSuccess": true,
  "entity": {
    "name": "category1",
    "description": "category1Test",
    "udatetime": "2021-12-09T16:27:45.7578499+03:00",
    "uuser": 1
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

<!-- Created by UgurKiymetli -->

## Products

## Create a new Product

`POST /Product/`

<details>
  <summary>Request</summary>

```curl
curl --location --request POST 'https://localhost:44359/api/Product' \
     --header 'Content-Type: application/json' \
     --data-raw
     '{
          "categoryId": 1,
          "name": "product2",
          "displayName": "product2",
          "description": "product2",
          "price": 9999.9,
          "iuser": 1
     }'
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": {
    "categoryId": 1,
    "name": "product2",
    "displayName": "product2",
    "description": "product2",
    "price": 9999.9,
    "idatetime": "2021-12-07T22:52:24.7407267+03:00",
    "iuser": 1
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

## Get list of Products

`GET /api/product`

<details>
  <summary>Request</summary>

```curl
curl -X GET "https://localhost:44359/api/Product" -H  "accept: text/plain"
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "category": "category1",
      "name": "product1",
      "displayName": "product1",
      "description": "product1",
      "price": "999,90 ₺",
      "idatetime": "2021-12-07T17:20:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    },
    {
      "id": 2,
      "category": "category2",
      "name": "product2",
      "displayName": "product2",
      "description": "product2",
      "price": "999,90 ₺",
      "idatetime": "2021-12-07T17:21:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    }
  ],
  "totalCount": 1,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

## Update a Product

### Request

`PUT /Product/id`

<details>
  <summary>Request</summary>

```
curl --location
    --request PUT 'https://localhost:44359/api/Product/1002' \
    --header 'Content-Type: application/json' \
    --data-raw
    '{
        "categoryId": 1,
        "name": "product2",
        "displayName": "product2Test",
        "description": "product2",
        "price": 99,
        "stock": 99,
        "uuser": 1
    }'
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": {
    "categoryId": 1,
    "name": "product2",
    "displayName": "product2Test",
    "description": "product2",
    "price": 99,
    "stock": 99,
    "udatetime": null,
    "uuser": 11
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

<!-- Created by UgurKiymetli -->

## Users

## Create a new User

`POST /User`<details>

  <summary>Request</summary>

```curl
curl --location --request POST 'https://localhost:44359/api/User/register' \
     --header 'Content-Type: application/json' \
     --data-raw
     '{
          "name": "user",
          "username": "user1",
          "email": "user@mail.com",
          "password": "veryStrongPassword"
     }'
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": {
    "name": "user",
    "username": "user1",
    "email": "user@mail.com",
    "password": "veryStrongPassword"
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

## User Login

`POST /User/login`

<details>
  <summary>Request</summary>

```curl
curl --location --request POST 'https://localhost:44359/api/User/login' \
     --header 'Content-Type: application/json' \
     --data-raw
     '{
          "username": "user1",
          "password": "veryStrongPassword"
     }'

```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": {
    "username": "user1",
    "password": "veryStrongPassword"
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

## Get list of Users

`GET /User`

<details>
  <summary>Request</summary>

```
curl --location --request GET 'https://localhost:44359/api/User'
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "name": "user",
      "username": "user1",
      "email": "user@mail.com",
      "idatetime": "2021-12-07T22:53:24.7407267+03:00"
    },
    {
      "id": 2,
      "name": "user2",
      "username": "user2",
      "email": "user2@mail.com",
      "idatetime": "2021-12-07T23:30:24.7407267+03:00"
    }
  ],
  "totalCount": 2,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

## Update a User

`PUT /User/id`

<details>
  <summary>Request</summary>

```
curl --location
    --request PUT 'https://localhost:44359/api/User/1002' \
    --header 'Content-Type: application/json' \
    --data-raw
    '{
        "username": "user1",
        "email": "updatedUserMail@mail.com",
        "password": "updatedVeryStrongPassword",
        "uuser": 1
    }'
```

</details>
<details>
  <summary>Response</summary>

```json
{
  "isSuccess": true,
  "entity": {
    "username": "user1",
    "email": "updatedUserMail@mail.com",
    "password": "updatedVeryStrongPassword",
    "udatetime": "2021-12-09T16:27:45.7578499+03:00",
    "uuser": 11
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

</details>

<!-- Created by UgurKiymetli -->

<!---

## Get a specific Thing



### Request

`GET /thing/id`

    curl -i -H 'Accept: application/json' http://localhost:7000/thing/1

### Response

    HTTP/1.1 200 OK
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 200 OK
    Connection: close
    Content-Type: application/json
    Content-Length: 36

    {"id":1,"name":"Foo","status":"new"}

## Get a non-existent Thing

### Request

`GET /thing/id`

    curl -i -H 'Accept: application/json' http://localhost:7000/thing/9999

### Response

    HTTP/1.1 404 Not Found
    Date: Thu, 24 Feb 2011 12:36:30 GMT
    Status: 404 Not Found
    Connection: close
    Content-Type: application/json
    Content-Length: 35

    {"status":404,"reason":"Not found"}

-->

<!-- Created by UgurKiymetli -->

## Delete a Thing _(Works Same For All Entities)_

### Request

`DELETE /Thing/id`

```
curl --location --request DELETE 'https://localhost:44359/api/Thing/1'
```

### Response

```json
{
  "isSuccess": true,
  "entity": {
    "thing-key1": "thing-value1",
    "thing-key2": "thing-value2",
    "thing-key3": "thing-value3",
    "thing-key4": "thing-value4",
    "thing-key5": "thing-value5"
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Try to delete same Thing again

### Request

`DELETE /Thing/id`

```
curl --location --request DELETE 'https://localhost:44359/api/Thing/1'
```

### Response

```json
{
  "isSuccess": false,
  "entity": null,
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": "Thing with id: 1 is not found"
}
```

<!-- ## Get deleted Thing

### Request

`GET /thing/1`

    curl -i -H 'Accept: application/json' http://localhost:7000/thing/1

### Response

    HTTP/1.1 404 Not Found
    Date: Thu, 24 Feb 2011 12:36:33 GMT
    Status: 404 Not Found
    Connection: close
    Content-Type: application/json
    Content-Length: 35

    {"status":404,"reason":"Not found"}
 -->

<!-- Created by UgurKiymetli -->

</details>
<br>

## Week 4 - Assignment

# Auth

## Login

- User **must** be logged in to use all of the other endpoints. If user is not logged in it returns error.

```
Please login!
```

### Request

`GET Auth/login`

```
curl --location --request POST 'https://localhost:44359/api/Auth/login' \
     --header 'Content-Type: application/json' \
     --data-raw
     '{
         "username": "user1",
          "password": "veryStrongPassword"
      }'
```

### Response

```json
{
  "isSuccess": true,
  "entity": {
    "id": 11,
    "name": "user1",
    "username": "user1",
    "email": "user@mail.com",
    "idatetime": "2021-12-06T22:43:28.433",
    "udatetime": null,
    "iuser": "1"
  },
  "list": null,
  "totalCount": 0,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Logout

### Request

`GET Auth/logout`

```
curl -X GET "https://localhost:44359/api/Auth/logout" -H  "accept: text/plain"
```

### Response

```json
{{
    "isSuccess": true,
    "entity": false,
    "list": null,
    "totalCount": 0,
    "validationErrorList": null,
    "exceptionMessage": null
}
```

## Pagination

- MaxPageSize can be set in the Infrastructure/PaginationFilter. Default is 15.
- Default page number is set to 1.

### Request

`GET Product?pageSize=1&pageNumber=1`

```
curl --location --request GET 'https://localhost:44359/api/Product?pageSize=1&pageNumber=1'
```

### Response

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "category": "category1",
      "name": "product1",
      "displayName": "product1",
      "description": "product1",
      "price": "999,90 ₺",
      "idatetime": "2021-12-07T17:20:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    }
  ],
  "totalCount": 1,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Sorting

- SORTING_PARAMETER can be set to price, name, displayName, stock, idatetime etc. (some might not work :/ )
- Default sorting is set to ascending.
- Changing sorting to descending is not supported yet.

### Request

`GET Product?sorting={SORTING_PARAMETER}`

```
curl --location --request GET 'https://localhost:44359/api/Product?sorting={SORTING_PARAMETER}'
```

### Response

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "category": "category1",
      "name": "product1",
      "displayName": "product1",
      "description": "product1",
      "price": "999,90 ₺",
      "idatetime": "2021-12-07T17:20:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    },
    {
      "id": 1,
      "category": "category1",
      "name": "product2",
      "displayName": "product2",
      "description": "product2",
      "price": "1999,90 ₺",
      "idatetime": "2021-12-07T17:20:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    }
  ],
  "totalCount": 2,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

## Paging and Sorting

### Request

`GET Product?pageSize=1&pageNumber=1&sorting={SORTING_PARAMETER}`

```
curl --location --request GET 'https://localhost:44359/api/Product?pageSize=1&pageNumber=1&sorting={SORTING_PARAMETER}'
```

### Response

```json
{
  "isSuccess": true,
  "entity": null,
  "list": [
    {
      "id": 1,
      "category": "category1",
      "name": "product1",
      "displayName": "product1",
      "description": "product1",
      "price": "999,90 ₺",
      "idatetime": "2021-12-07T17:20:29.057",
      "udatetime": "0001-01-01T00:00:00",
      "iuser": "user1"
    }
  ],
  "totalCount": 1,
  "validationErrorList": null,
  "exceptionMessage": null
}
```

### Tech Stack

**Backend:** .Net Core / **Frontend:** ReactJS / **Database:** MSSql

### Acknowledgements

- [Awesome Readme Templates](https://awesomeopensource.com/project/elangosundar/awesome-README-templates)
- [Awesome README](https://github.com/matiassingers/awesome-readme)
- [How to write a Good readme](https://bulldogjob.com/news/449-how-to-write-a-good-readme-for-your-github-project)

<!-- Created by UgurKiymetli -->
