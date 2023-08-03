<template>
  <el-text size="large" tag="b">医生团队</el-text>
  <div width=50%>
    <el-carousel :interval="4000" type="card" height="200px" max-width="200" >
      <div>
        <el-carousel-item v-for="item in 3" :key="item">
       <img  class="imgdoctor" style="height:200px;width:500px" src="https://img0.baidu.com/it/u=2912136480,2206555971&fm=253&fmt=auto&app=120&f=JPEG?w=1200&h=800">
     <!-- <el-text>hahahaha</el-text> -->
   </el-carousel-item>
      </div> 
    </el-carousel>
  </div>

  </template>
  
  <style scoped>
.el-carousel{
  max-width:90%
}
  
  .el-carousel__item:nth-child(2n) {
    background-color: #99a9bf;
    border-radius: 10px;
    box-shadow: 0 0px 2px rgba(0, 0, 0, .2);
 
  }
  
  .el-carousel__item:nth-child(2n + 1) {
    background-color: #d3dce6;
    border-radius: 10px;
    box-shadow: 0 0px 2px rgba(0, 0, 0, .2);

  }

  </style>