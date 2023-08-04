<template>
<el-dropdown class="my-trigger" trigger="click">
    <span>
       品种 
          <el-icon >
        <ArrowDownBold />
      </el-icon>
    </span>

    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item>猫猫</el-dropdown-item>
        <el-dropdown-item>狗狗</el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>


  <el-dropdown class="my-trigger" style="margin-left: 20px;" trigger="click">
    <span  >
       接种情况
       <el-icon><CaretBottom /></el-icon>
    </span>
    <template #dropdown>
      <el-dropdown-menu>
        <el-dropdown-item>已接种</el-dropdown-item>
        <el-dropdown-item>未接种</el-dropdown-item>
      </el-dropdown-menu>
    </template>
  </el-dropdown>


  <el-row :gutter="20" style="margin-top: 20px;">
    <el-col :span="6">
<petcardnew :petInfo.name="petName" :petInfo.breed="petBreed" :petInfo.age="petAge" :petInfo.image-path="petImagePath"/>
</el-col>
 <el-col :span="6"><petcardnew :name="petName" :breed="petBreed" :age="petAge" :imagePath="petImagePath"/></el-col>
 <el-col :span="6"><petcardnew :name="petName" :breed="petBreed" :age="petAge" :imagePath="petImagePath"/></el-col>
 <el-col :span="6"><petcardnew :name="petName" :breed="petBreed" :age="petAge" :imagePath="petImagePath"/></el-col>
  </el-row>
  
</template>

<script>
import petcardnew from './petcardnew.vue'
import {
 ArrowDown,ArrowDownBold,CaretBottom
} from '@element-plus/icons-vue'
export default {
  components: {
    petcardnew
  },
  data() {
    return {
      petName: 'Tom',
      petBreed: 'Cat',
      petAge: 3,
      petImagePath: 'bomei.jpg'
    }
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
