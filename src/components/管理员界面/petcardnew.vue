<template>
    <el-card class="pet-card" :body-style="{ padding: '0px' }">
    <template v-if="showText">
        <div class="image" >
        <div>{{ petInfo.props.name }}</div>
        <div>{{ petInfo.props.breed }}</div>
        <div>{{ petInfo.props.age }}</div>
        </div>
    </template>
    <template v-else>
    <img src=@/photos/petInfo.props.imagePath  class="image"/>
    </template>
        <div style="padding: 14px">
            <span>Birthday Puppy</span>
            <div class="bottom">
            <el-button text  class="button" @click="viewPetInfo">查看</el-button>
            <el-button text  class="button">编辑</el-button>
            </div>
         </div>
    </el-card>
</template>
    
    <script>
    export default {
      data() {
        return {
          showText:false,
          petInfo: {
            props: {
                name:String,
                breed: String,
                age: Number,
                imagePath: String
            }
          }
        };
      },
      methods: {
        viewPetInfo() {
          this.showText=!this.showText
        }
      }
    };
    </script>
    
    <style>
    .image {
      margin-top: 10px;
      margin-left:10px;
      min-width: 200px;
      display: block;
      min-height:270px
    }
    </style>