## 普通用户网站访问地址
http://101.42.19.77:3000/
## 开发者访问方法
首先clone下来本仓库
```bash
git clone https://github.com/TongjiPetWelfareProject/DB_PetWelfare
cd DB_PetWelfare
```
然后用vscode打开项目文件。
```bash
npm install
npm run build --if-present
npm run dev
```
如果想要在本地与后端一起运行，***那么需要修改http.js与vite.node.js中的101.42.19.77为localhost***
## 前端文件服务器配置方法
详见 [Nginx配置方法](https://github.com/TongjiPetWelfareProject/DB_PetWelfare/wiki/Nginx%E9%85%8D%E7%BD%AE%E6%96%B9%E6%B3%95)
## 数据库服务器配置方法
详见[数据库服务器配置方法](https://github.com/TongjiPetWelfareProject/DB_PetWelfare/wiki/%E6%95%B0%E6%8D%AE%E5%BA%93%E6%9C%8D%E5%8A%A1%E5%99%A8%E9%85%8D%E7%BD%AE%E6%96%B9%E6%B3%95)
## 后端文件使用方法
后端文件位于[同济宠物中心后端](https://github.com/TongjiPetWelfareProject/TongjiPetWelfare)
，同样地将仓库clone下来，
然后配置环境变量MYDATABASE为

```User Id=C##PET;Password=campus;Data Source=*.*.*.*:1521/orcl```

同时将values.json文件放在后端文件夹中，然后就可以启动后端了。
## CONTRIBUTE TO US
请fork一个仓库，完成修改后创建PR等待维护者合并。
## 协议
本仓库采用GPL系列协议，使用必须标注版权 Copyright@TongjiPetWelfareProject，并且需要注明自己所做的更改。
## 本地数据库配置
请参照database或者项目设计方案中的README文件与sql文件配置。
