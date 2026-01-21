USE NZWalksDB
GO

SELECT *
FROM Walks
GO

SELECT *
FROM Difficulties
GO

SELECT *
FROM Regions
GO

INSERT INTO Regions
VALUES
  (
    '35b76290-1889-4915-86cc-3534d77ed761',
    'AUK',
    'Auckland',
    'https://i.pinimg.com/1200x/d0/fc/f3/d0fcf30de9d249ab8d02391fcc678cd2.jpg'
),
  (
    'a06e1ddb-83bf-4ffc-aac1-7b7c4bf7f822',
    'NTL',
    'Northland',
    'https://i.pinimg.com/736x/24/df/0e/24df0e95d0f76a668a680ccf0a88fcdb.jpg'
),
  (
    'ae96acd9-49eb-480c-ab85-4a372e961227',
    'QNT',
    'Queenstown',
    'https://i.pinimg.com/1200x/0a/d6/26/0ad62681527a91b9054c1a362cc64228.jpg'
),
  (
    '213785ed-8bc9-46ac-84c4-885abe3583e8',
    'WGN',
    'Wellington',
    'https://i.pinimg.com/1200x/f8/8e/b4/f88eb4cc51cef11c92ad4fb0492673c7.jpg'
),
  (
    '974a1b4e-939b-4dbb-b798-4a7924b947d0',
    'HLZ',
    'Hamilton',
    'https://i.pinimg.com/1200x/e0/82/98/e08298d1c4f3d29b92c15265660c4b4b.jpg'
),
  (
    '4f91fec9-4b07-4bf6-8d81-e6a7430f19c2',
    'ROT',
    'Rotorua',
    'https://i.pinimg.com/1200x/a4/45/39/a44539536298c325706db782491167c3.jpg'
)
 GO




-- DELETE 
-- DELETE
-- FROM Regions
-- GO
