# Kyle Emery
## Software Design III
### Assignment 01 - Controllers & Routes

#### Re-populate the database by running the bash script using the following bash terminal command:
./repopDb.sh

Then manually run the query from /Data/chinook_data.sql

#### Album Controller
- Get All Albums [GET api/album]
- Get Album by ID [GET api/album{albumId}]
- Delete Album [DELETE api/album{albumId}]
- Search for Album [GET api/album/search]
  + Search by Title
  + Search by Artist Name
  + Does NOT allow BOTH to be input at the same time
- Get Albums by Artist [GET api/album/albums-by-artist]

#### Artist Controller
- Get All Artists [GET api/artist]
  + Missing albums
- Get Artist by ID [GET api/artist/{artistId}]
  + Missing albums
- Delete Artist [DELETE api/artist/{artistId}]
- Get Artist Stats [GET api/artist/stats]

#### Customer Controller
- Get All Customers [GET api/customer]
  + Missing support rep include
- Get Customer by ID [GET api/customer/{customerId}]
- Delete Customer [DELETE api/customer/{customerId}]

#### Invoice Controller
- Get All Invoices [GET api/invoice]
  + Missing customer
- Get Invoice by ID [GET api/invoice/{invoiceId}]
  + Missing customer
- Delete Invoice [DELETE api/invoice/{invoiceId}]
- Get Invoice Stats [GET api/invoice/stats]
  + Missing customer

#### Playlist Controller
- Get All Playlists [GET api/playlist]
  + Missing tracks
- Get Playlist by ID [GET api/playlist/{playlistId}]
  + Missing customer
- Delete Playlist [DELETE api/playlist/{playlistId}]
- Get Expensive Playlist [GET api/playlist/top-expensive]