# Kyle Emery
## Software Design III
### Assignment 01 - Controllers & Routes

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
- Get Artist by ID [GET api/artist/{artistId}]
- Delete Artist [DELETE api/artist/{artistId}]
- Get Artist Stats [GET api/artist/stats]
  + THIS IS NOT DONE YET

#### Customer Controller
- Get All Customers [GET api/customer]
- Get Customer by ID [GET api/customer/{customerId}]
- Delete Customer [DELETE api/customer/{customerId}]

#### Invoice Controller
- Get All Invoices [GET api/invoice]
- Get Invoice by ID [GET api/invoice/{invoiceId}]
- Delete Invoice [DELETE api/invoice/{invoiceId}]
- Get Invoice Stats [GET api/invoice/stats]
  + THIS IS NOT DONE YET

#### Playlist Controller
- Get All Playlists [GET api/playlist]
- Get Playlist by ID [GET api/playlist/{playlistId}]
- Delete Playlist [DELETE api/playlist/{playlistId}]
- Get Expensive Playlist [GET api/playlist/top-expensive]
  + THIS IS NOT DONE YET