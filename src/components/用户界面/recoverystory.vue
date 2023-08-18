<template>
  <div class="demo-collapse">
    <br>
    <div style="display:flex;align-items: left;justify-content: left">
    <div style="display:block">
      <div>
      <p style="font-size: 24px;font-weight: bold;color:#729cd4;">科室介绍</p>
    </div>
    
    <div style="display:block;text-align: center;justify-content: center;">
      <p style="margin-bottom:1px;color: #b4bac2;font-size: 14px;">皮肤感染、胃部、疼痛、肿块...我们尽全力帮助宠物脱离痛苦。</p>
    </div>
    </div>
    <img src="@/photos/rcstry.png" style="margin-left:200px;width:250px;height:120px">
  </div>
    <br>
    <el-collapse v-model="activeName" max-height="200" accordion>
      <el-collapse-item v-for="story in recoveryStories" :key="story.id" :title="story.name" :name="story.id">
        <div>
          {{ story.content }}
        </div>
      </el-collapse-item>
    </el-collapse>
  </div>
  <!-- <el-pagination layout="prev, pager, next" :total="50" /> -->
</template>

<script  setup>
import { ref, onMounted } from 'vue';
import medical_donate from '@/api/medical_donate';

const activeName = ref('');

// onMounted(async () => {
//   try {
//     const stories = await medical_donate.recoveryStoryAPI();
//     recoveryStories.value = stories;
//   } catch (error) {
//     console.error('获取康复故事数据时出错：', error);
//   }
// });

const recoveryStories = ref([
  {
    id: '1',
    name: '皮肤问题',
    content: '一只患重感冒的小猫，在医生的细心治疗下，终于战胜病魔，重新恢复了活泼可爱的模样。'
  },
  {
    id: '2',
    name: '胃部问题',
    content: '一只遭受车祸的柴犬旺财，经历了十字韧带撕裂手术和康复训练，如今她早已重获新生，快乐地在花园奔跑。'
  },
  {
    id: '3',
    name: '耳部感染',
    content: '一只病弱的流浪猫花宝，在志愿者的照料下，经过多次药物治疗和调理，逐渐康复成为健康的家庭成员。'
  },
  {
    id: '4',
    name: '眼部疾病',
    content: '生病的比熊叮当，在医生团队的努力下，经过手术和护理，成功摆脱过敏反应，重新焕发生机。'
  },
  {
    id: '5',
    name: '疼痛',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  },
  {
    id: '6',
    name: '肿块',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  },
  {
    id: '7',
    name: '泌尿道感染',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  },
  {
    id: '8',
    name: '过敏反应',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  },
  {
    id: '9',
    name: '十字韧带撕裂和手术',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  },
  {
    id: '10',
    name: '癌症',
    content: '受伤的流浪小狗酷酷，在好心人的帮助下，得到了及时的医疗和关爱，最终康复健康，摆脱了耳部感染等五种疾病。'
  }
]);


</script>

  
  <style>
  .el-collapse{
    max-width:90%;
  }

  .demo-collapse{
    border-top: 1px solid #dad9d9;
    margin-left:20px;
  }


</style>