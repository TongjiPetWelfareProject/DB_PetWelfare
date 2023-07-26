<script lang="ts" setup>
import { ref, computed } from 'vue'
// import image1 from './pet1.jpg'
// import image2 from './photos/pet2.jpg'
// import image3 from './photos/pet3.jpg'
// import image4 from './photos/pet4.jpg'
// import image5 from './photos/pet5.jpg'

const pets = [
  { id: 1, name: '汤姆', breed: '狗', gender: '弟弟', age: 1},
  { id: 2, name: 'Honey', breed: '狗', gender: '妹妹', age: 3},
  { id: 3, name: 'Kitty', breed: '猫', gender: '弟弟', age: 5},
  { id: 4, name: '旺财', breed: '狗', gender: '妹妹', age: 7},
  { id: 5, name: '球球', breed: '猫', gender: '弟弟', age: 9},
];

const value1 = ref('');
const value2 = ref('');
const value3 = ref('');

const ageRangeMatches = (petAge: number, ageRange: string) => {
  const [min, max] = ageRange.split('-').map(Number);
  return petAge >= min && petAge <= max;
};

const filteredPets = computed(() => {
  return pets.filter(pet => {
    return (
      (value1.value === '' || pet.breed === value1.value) &&
      (value2.value === '' || pet.gender === value2.value) &&
      (value3.value === '' || ageRangeMatches(pet.age, value3.value))
    );
  });
});

const options1 = [
  { value: '', label: '不限' },
  { value: '猫', label: '猫' },
  { value: '狗', label: '狗' },
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
    <div class="pet-list-container">
      <div class="pet-list">
        <div v-for="pet in filteredPets" :key="pet.id" class="pet-card">
          <div class="pet-image">
            <!-- <img :src="pet.image" alt="宠物图片" /> -->
          </div>
          <div class="pet-details">
            <h2>{{ pet.name }}</h2>
            <p>品种: {{ pet.breed }}</p>
            <p>性别: {{ pet.gender }}</p>
            <p>年龄: {{ pet.age }}</p>
            <el-button type="primary" plain>查看详情</el-button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.background-container {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  display: flex;
  flex-direction: column; /* 将子元素垂直排列 */
  justify-content: center; /* 在容器中垂直居中对齐 */
  align-items: center; /* 在容器中水平居中对齐 */
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
}
.filters-container {
  width: 80%;
  background-color: rgba(166, 219, 225, 0.8);/* 设置背景颜色 */
  padding-top: 20px; /* 添加顶部内边距 */
  padding-bottom: 20px; /* 添加底部内边距 */
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
  box-shadow: 0 0 3px #007bff; /* 添加聚焦时的阴影效果 */
}

.pet-list-container {
  height: 70%; /* 设置容器的高度 */
  width: 80%;
  overflow-y: auto; /* 设置垂直滚动条 */
}

.pet-list {
  display: flex;
  flex-wrap: wrap;
  justify-content: space-between;
}

.pet-card {
  display: flex;
  height: 200px;
  width: 48%; /* 设置每个宠物卡片的宽度为占比的48% */
  justify-content: space-between; /*子元素平均分布*/
  margin-bottom: 10px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.5); /* 添加黑色阴影底板 */
	background-color: #7A7A7A; 
	background-color: rgba(166, 219, 225, 0.8); /* 设置底板为半透明的 #7A7A7A */
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
</style>
