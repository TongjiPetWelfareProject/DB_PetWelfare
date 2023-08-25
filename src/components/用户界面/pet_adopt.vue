<script lang="ts" setup>
import { ref, computed, onMounted } from 'vue'
import petadopt from '@/api/pet_adopt'

const images = [//等后端图片，后期去掉
'./src/components/photos/pet1.jpg',
'./src/components/photos/pet2.jpg',
'./src/components/photos/pet3.jpg',
'./src/components/photos/pet4.jpg',
'./src/components/photos/pet5.jpg',
];

const pets = ref([])
const getpetlist = async () => {
  try {
    const response = await petadopt.getPetList();
    for (const adoptpet of response) {
      let gender = '';
      if (adoptpet.SEX === 'M') {
        gender = '弟弟';
      } else if (adoptpet.SEX === 'F') {
        gender = '妹妹';
      }
      console.log(adoptpet.PET_NAME)
      pets.value.push({
        id: adoptpet.PET_ID,
        name: adoptpet.PET_NAME,
        breed: adoptpet.SPECIES,
        gender: gender,
        age: adoptpet.AGE,
        image: images[0]
      });
    }
  } catch (error) {
    console.error('获取可领养宠物数据时出错：', error);
  }
};


onMounted(() => {
  getpetlist();
});

console.log(pets.value);

const value1 = ref('');
const value2 = ref('');
const value3 = ref('');
let min = 0;
let max = 100;
const ageRangeMatches = (petAge: number, ageRange: string) => {
  if (ageRange != '11') {
    [min, max] = ageRange.split('-').map(Number);
  } else {
    min = 11; // 默认的最小值
    max = 100; // 默认的最大值
  }
  return petAge >= min && petAge <= max;
};

const filteredPets = computed(() => {
  return pets.value.filter(pet => {
    return (
      (value1.value === '' || pet.breed === value1.value) &&
      (value2.value === '' || pet.gender === value2.value) &&
      (value3.value === '' || ageRangeMatches(pet.age, value3.value))
    );
  });
});

const options1 = [
  { value: '', label: '不限' },
  { value: 'cat', label: '猫' },
  { value: 'dog', label: '狗' },
];
const options2 = [
  { value: '', label: '不限' },
  { value: '弟弟', label: '弟弟' },
  { value: '妹妹', label: '妹妹' },
];
const options3 = [
  { value: '', label: '不限' },
  { value: '0-1', label: '0-1岁' },
  { value: '2-3', label: '2-3岁' },
  { value: '4-7', label: '4-7岁' },
  { value: '8-10', label: '8-10岁' },
  { value: '11', label: '11岁及以上' },
];

</script>

<template>
  <div style="display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; /* 在垂直方向上居中 */
  /* 其他样式 */">
  <el-row :gutter="20" style="height: 500px; width: 100%; background-color:#4b6fa5;display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; 
  ">
    <el-col :span="12" style="height:80%; display: flex;
  justify-content: center; /* 在水平方向上居中 */
  align-items: center; /* 在垂直方向上居中 */">
       <div style="background-color: #4b6fa5;height:95%;padding:20px;border-radius: 10px;">
        <br>
        <br>
         <div style="color: #ccdff5;font-size: 35px;">
          宠物领养
         </div>
         <div style="color: #ffffff;font-size: 50px;font-family:fantasy">
          一段奇妙的生命旅程
         </div>
         <br>
         <div style="color: #ffffff;font-size: 16px;font-family:cursive">
          有一只在角落躲闪的小家伙，正用心静静等待那个将牵起牵绊的人
         </div>
         <div style="color: #ffffff;font-size: 16px;font-family:cursive">
          让我们一同为宠物们开启一扇幸福的大门，让爱在这里延续，温馨永远蔓延
         </div>
        
       </div>
    </el-col>
    <el-col :span="12">
      <img src="./photos/狗互动.jpg" style=" height: 500px;">
    </el-col>
  </el-row></div>
  <div class="background-container">
    <div class="filters-container">
        <label>品种:</label>
        <el-select v-model="value1" placeholder="选择品种">
          <el-option
            v-for="item in options1"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <label>性别:</label>
        <el-select v-model="value2" placeholder="选择性别">
          <el-option
            v-for="item in options2"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
        <label>年龄:</label>
        <el-select v-model="value3" placeholder="选择年龄范围">
          <el-option
            v-for="item in options3"
            :key="item.value"
            :label="item.label"
            :value="item.value">
          </el-option>
        </el-select>
    </div>
    <br>
    <div class="pet-list-container">
      <div class="pet-list">
        <!-- <div v-for="pet in filteredPets" :key="pet.id" class="pet-card">
          <div class="pet-image">
            <img :src="pet.image" alt="宠物图片" />
          </div>
          <div class="pet-details">
            <h2>{{ pet.name }}</h2>
            <p>品种: {{ pet.breed }}</p>
            <p>性别: {{ pet.gender }}</p>
            <p>年龄: {{ pet.age }}</p>
            <router-link to="/pet_details">查看详情
        </router-link>
          </div>
        </div> -->
        <div v-for="pet in filteredPets" :key="pet.id" class="pet-card">
          <el-card :body-style="{ padding: '0px' }" style="width:100%">
      <img src="../../../public/home5.jpg" class="adopt_image">
      <div style="padding: 14px;display: flex;
  justify-content: center;
  flex-direction: column; ">
        <span style="font-size: 18px; color:#4b6fa5;font-weight: bold;">你好呀！我的名字是</span>
        <span style="font-size: 40px; color:#edb055;font-weight: bold;">{{ pet.name }}</span>
        <br>
        <span style="font-size: 18px">{{ pet.gender }}</span>
        <span style="font-size: 16px; color:#6b6a68">{{ pet.age }}岁</span>
        <br>
      <router-link :to="{ name: 'pet_details', params: { id: pet.id } }" style=" text-decoration: none;
  color:#edb055 ;">
    查看详情  —>
  </router-link>
      </div>
    </el-card>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.background-container {
  /* position: fixed; */
  /* top: 0;
  left: 0;
  right: 0;
  bottom: 0; */
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  justify-content: center; /* 在容器中垂直居中对齐 */
  align-items: center; /* 在容器中水平居中对齐 */
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}
.filters-container {
  width: 100%;
  background-color:#eaebeb;/* 设置背景颜色  rgba(166, 219, 225, 0.8)*/
  padding-top: 10px; /* 添加顶部内边距 */
  padding-bottom: 10px; /* 添加底部内边距 */
  border-radius: 4px; /* 设置圆角 */
  margin-bottom: 10px;
  display: flex; /* 使用 flex 布局 */
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
}

.filters-container > * { /*选择.filters-container元素的所有直接子元素*/
  margin-left: 10px; /* 设置子元素的左边距为10像素 */
  margin-right: 10px; /* 设置子元素的右边距为10像素 */
}

select {
  appearance: none; /* 移除默认样式 */
  padding: 8px; /* 调整内边距 */
  border: none; /* 移除边框 */
  border-radius: 4px; /* 设置圆角 */
  background-color: rgb(213, 136, 49); /* 设置背景颜色为橙色 */
  color: #fff; /* 设置文字颜色为白色 */
  font-size: 14px; /* 设置字体大小 */
}

/* 鼠标悬停样式 */
select:hover {
  background-color: rgb(255, 165, 0); /* 设置悬停时的背景颜色 */
}

/* 聚焦样式 */
select:focus {
  outline: none; /* 移除聚焦时的外边框 */
  box-shadow: 0 0 3px #729cd4; /* 添加聚焦时的阴影效果 */
}

.pet-list-container {
  height: 70%; /* 设置容器的高度 */
  width: 100%;
  overflow-y: auto; /* 设置垂直滚动条 */
}

.pet-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.pet-card {
  display: flex;
  /* height: 200px; */
  width: 24%; /* 设置每个宠物卡片的宽度为占比的48% */
  justify-content: space-between; /*子元素平均分布*/
  margin-bottom: 10px;
  border-radius: 4px; /* 设置圆角 */
}

.pet-image {
  display: flex; /* 使用flex布局 */
  justify-content: center; /* 水平居中 */
  align-items: center; /* 垂直居中 */
  margin-right: 10px; /* 设置宠物图片区域右边距为 10px */
  /* 添加以下样式 */
  width: 200px; /* 设置宠物图片区域的宽度为 100px */
  height: 200px; /* 设置宠物图片区域的高度为 100px */
  overflow: hidden; /* 隐藏溢出部分的图片内容 */
}

.pet-image img {
  width: 80%; /* 设置宠物图片的宽度为容器的100% */
  height: 80%; /* 设置宠物图片的高度为容器的100% */
  object-fit: cover; /* 拉伸图片以填充容器，保持宽高比例 */
  border-radius: 50%; /* 设置边框半径为50%，将图片裁剪为圆形 */
}

.pet-details {
  flex: 1; /* 设置其他元素区域占据剩余的可用空间 */
  margin-bottom: 0px; /* 调整每个段落元素（品种、性别、年龄、人气）的底部边距 */
  line-height: 10px;
}

.pet-details p {
  padding-bottom: 0px; 
}

.title{
  height:300px;
  background-image: url("./photos/寄养背景.jpg");
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
  display: flex;
}
.adopt_image {
    width: 100%;
    display: block;
  }

</style>
