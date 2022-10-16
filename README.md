# 高雄寵物地圖
> 此網頁主要為了可以搜尋資料庫裡蒐集的高雄寵物餐廳以及寵物友善餐廳而設計的網站，可以更方便寵物奴搜尋高雄當地的寵物餐廳。
> 另外還加設了購物團購功能，可以在使用者瀏覽的同時，看到可能有興趣的產品，可以下訂商品。

<br><br>

## 首頁
![未命名](https://user-images.githubusercontent.com/114054051/196020445-ba234065-5348-4a2e-845a-f72889b4fb6a.png)
> 進入首頁，版面上方的NavBar清楚顯示所有功能。在未登入前，顯示註冊字樣，登入後會顯示會員中心。

<br><br>
## 搜尋地圖功能
![未命名3](https://user-images.githubusercontent.com/114054051/196020631-1ea8f366-65e2-45da-85d2-1d07063e21d7.png)
> 因網路上的資源較雜，所以才想利用資料庫去蒐集只有一個地區也就是高雄的寵物餐廳，利用controller去撰寫搜尋功能，主要是利用區域去搜尋。
> 點擊地圖，會跳轉到google地圖搜尋該餐廳的資訊。

![image](https://user-images.githubusercontent.com/114054051/196021598-859385b3-9f71-42a3-b27c-18a2c6a49ac0.png)
> 搜尋功能舉例，搜尋框內輸入「前鎮」，就會顯示前鎮區的餐廳。

![未命名9](https://user-images.githubusercontent.com/114054051/196020847-a43a457b-bb86-4796-be52-9efaf8d0b70c.png)
> 點擊詳細會顯示餐廳的詳細資訊，利用bootstrap裡的modal呈現。

<br><br>
## 會員功能
![未命名2](https://user-images.githubusercontent.com/114054051/196020599-04205f74-fca3-4331-99af-53f0623c4815.png)
> 註冊會員頁面，有設置相關安全限制等提示

![image](https://user-images.githubusercontent.com/114054051/196021557-47f24745-d45c-4956-ac0b-ebaa8e63bae9.png)
> 會員登入頁面

![image](https://user-images.githubusercontent.com/114054051/196021571-1a4de31a-3959-441b-9a22-2924d873d814.png)
> 會員登入成功會跳轉到團購頁面，並顯示「登入成功！」提示框，因團購功能只有登入會員才能使用，所以才會有這樣的設定。

<br><br>
## 購物功能
![image](https://user-images.githubusercontent.com/114054051/196021704-fcb0bb9e-1ffe-4016-ab9c-6c41bf181962.png)
> 點選下方購物車圖示會新增商品至購物車，並顯示「已加入購物車」提示框，確保使用者確定自己已經加入購物車。

![image](https://user-images.githubusercontent.com/114054051/196021735-cf9eba3c-1874-4973-a540-a86e12377117.png)
> 若新增到相同商品，會直接更改localstorage的數量，並顯示「商品數量已更新」提示框。

![image](https://user-images.githubusercontent.com/114054051/196021762-dfb3ad50-73b7-4b0e-b6fb-a5572cd459a8.png)
> 購物車清單確認頁面，也可以在此頁面更改或刪除產品。

![image](https://user-images.githubusercontent.com/114054051/196021776-28692a4a-78c5-492f-9af4-05ed31c3a82f.png)
> 點選結帳會出現填寫配送資料的表格，填寫完成以後按確認送出訂單按鈕，會跳提示是否送出訂單？確認以後會再跳提示「結帳完成！」。

![image](https://user-images.githubusercontent.com/114054051/196021836-fc02cf94-171f-44f4-90d9-744993e0e3cd.png)
![image](https://user-images.githubusercontent.com/114054051/196021853-cc25b43d-0fe2-4c5b-a390-159f323209da.png)
> 在會員中心可以看到訂單明細以及會員資料

<br><br>

## 後台功能
