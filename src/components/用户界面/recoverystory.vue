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
    <img src="../../../public/rcstry.png" style="margin-left:40vw;width:250px;height:120px">
  </div>
    <br>
    <el-collapse v-model="activeName" max-height="200" >
      <el-collapse-item :header-class="custom-header" v-for="story in recoveryStories" :key="story.id" :title="story.name" :name="story.id">
        <div>
          {{ story.content }}
        </div>
      </el-collapse-item>
    </el-collapse>
    <br>
    <el-divider></el-divider>

    <div class="medicalprice">
      <div class="priceimg">
        <img src="ambulance.png" style="height: 200px;width:200px" alt="puppy">
      </div>
      <div class="pricetext">
        <p class="pricetext1">费用说明</p>
        <span class="pricetext2">猫、小型犬、中型犬每次20元，大型宠物每次50元。
          <br>手术费等其他额外费用将另行计算。</span>
      </div>
      
    </div>




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
    content: '宠物可能因过敏、感染等引发皮肤问题，我们提供皮肤专科治疗，让它们恢复健康的肌肤与毛发。'
  },
  {
    id: '2',
    name: '胃部问题',
    content: '不当的饮食会导致宠物胃部不适，可能影响食欲和健康，我们通过专业的诊断和治疗，帮助它们重新享受美味的食物。'
  },
  {
    id: '3',
    name: '耳部感染',
    content: '耳朵是宠物与外界交流的纽带，耳部问题可能令宠物耳朵发痒，甚至引发疼痛和炎症,但经过我们的护理，它们可以摆脱耳部感染问题。'
  },
  {
    id: '4',
    name: '眼部疾病',
    content: '眼睛疾病可能使宠物视力受损，它们的明亮眼神将黯淡无光。我们提供眼部治疗，帮助有眼疾的宠物能清楚地观察它们的世界。'
  },
  {
    id: '5',
    name: '疼痛',
    content: '不同程度的疼痛可能让宠物生活困难，我们提供不同部位的疼痛处理，让宠物能够重新快乐自在地运动和活动。'
  },
  {
    id: '6',
    name: '肿块',
    content: '未知的病变令人不安，宠物体内肿块可能需要专业的手术治疗。我们提供专业手术和康复，帮助它们恢复健康。'
  },
  {
    id: '7',
    name: '泌尿道感染',
    content: '泌尿道感染可能令宠物难以控制尿液，为宠物和主人带来不便和不适。我们提供宠物泌尿道治疗，确保宠物泌尿系统健康。'
  },
  {
    id: '8',
    name: '过敏反应',
    content: '过敏引发的皮肤瘙痒可能令宠物不停地抓挠，让它们烦恼。我们通过诊断过敏源并提供针对性治疗，减轻宠物的不适。'
  },
  {
    id: '9',
    name: '十字韧带撕裂和手术',
    content: '十字韧带撕裂使宠物丧失正常奔跑的乐趣，康复旅程漫长。十字韧带撕裂需要手术治疗，我们提供手术和对应的康复疗程，使宠物重获运动能力。'
  },
  {
    id: '10',
    name: '癌症',
    content: '癌症的威胁为宠物健康带来严峻挑战，需要大家的力量与支持共同面对。我们提供全方面综合性治疗，帮助宠物抵抗癌症，为宠物赋予更多的时光。'
  }
]);


</script>

  
  <style>
  .el-collapse{
    max-width:98%;
  }

  .demo-collapse{
    border-top: 1px solid #dad9d9;
    margin-left:2vw;
  }

  .el-collapse-item__header {
  font-size: 16px; /* 您希望的字体大小 */
  color: rgb(93, 93, 93);
}

.medicalprice{
  display:flex;
  align-items: center;
  justify-content: center;
}

.priceimg{
  display: block;
}

.pricetext{
  display: block;
  align-items: center;
  text-align: center;
  justify-content: center;
  margin-left: 40px;
}

.pricetext1{
  font-size: 23px;
  font-weight:  bold;
  color:#729cd4
}
.pricetext2{
  font-size: 15px;
 
  color:#5f6061
}

</style>