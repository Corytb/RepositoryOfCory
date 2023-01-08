USE IAmTraveling_DevDB

INSERT INTO ThingLocations (PlaceId, PlaceName, TypeOfPlace, ProvinceOrState, Country, FormattedAddress, LatitudeCoord, LongitudeCoord)
VALUES('TestLocationid 31', 'Wat Phetchabun', 'Temple', 'Nakhon Phathom', 'Thailand', '123 Phetchabun Street', 13.676424967804822, 101.06781699079103);

INSERT INTO Things (UserIdId, ThingLocationId, ThingDate, AddedToDbDate, UpdateDate, Description)
VALUES('02174cf0–9412–4cfe-afbf-59f706d72cf6', '1', '10/10/2022', '10/12/2022', '11/10/2022', 'This was such a cool day'),
('02174cf0–9412–4cfe-afbf-59f706d72cf6', '1', '11/10/2022', '11/12/2022', '12/10/2022', 'We went to Phetchabun');


INSERT INTO Comments (Text, CommentDate, ThingId)
VALUES('Hey, this is such a cool trip you did. Thanks for sharing!', '10/13/2022', 2),
('Yeah, thanks, it was a great day!', '10/13/2022', 2);

INSERT INTO MediaFiles (FileName, ThingId)
VALUES('testimage1.jpg', 2),
('testImage23.png', 2);