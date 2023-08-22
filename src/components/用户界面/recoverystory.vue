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
    <img src="../../../public/rcstry.png" style="margin-left:500px;width:250px;height:120px">
  </div>
    <br>
    <el-collapse v-model="activeName" max-height="200" >
      <el-collapse-item :header-class="custom-header" v-for="story in recoveryStories" :key="story.id" :title="story.name" :name="story.id">
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
    content: '宠物可能因过敏、感染等引发皮肤问题，我们提供专业医疗，让它们恢复健康的肌肤。'
  },
  {
    id: '2',
    name: '胃部问题',
    content: '宠物胃部不适可能影响食欲和健康，我们通过诊断和治疗，帮助它们重新享受美味的食物。'
  },
  {
    id: '3',
    name: '耳部感染',
    content: '耳朵问题可能令宠物耳朵发痒，甚至引发疼痛和炎,但经过护理，它们可以摆脱疼痛。'
  },
  {
    id: '4',
    name: '眼部疾病',
    content: '眼睛疾病可能使宠物视力模糊，它们的明亮眼神可能会变得黯淡。我们提供眼部治疗，帮助它们重现明亮的眼神。'
  },
  {
    id: '5',
    name: '疼痛',
    content: '疼痛可能让宠物生活困难，我们提供疼痛处理，确保宠物能够快乐自在地运动和活动。'
  },
  {
    id: '6',
    name: '肿块',
    content: '宠物体内肿块可能需要手术治疗，未知的病变或许令人不安。我们提供专业手术和康复，帮助它们恢复健康'
  },
  {
    id: '7',
    name: '泌尿道感染',
    content: '泌尿道感染可能令宠物难以控制尿液，带来不便和不适。我们提供治疗，确保宠物泌尿系统健康。'
  },
  {
    id: '8',
    name: '过敏反应',
    content: '过敏引发的皮肤瘙痒可能令宠物不安，不停地抓挠让它们烦恼。我们通过诊断过敏源并提供适当治疗，减轻宠物的不适。'
  },
  {
    id: '9',
    name: '十字韧带撕裂和手术',
    content: '十字韧带撕裂可能让宠物丧失正常奔跑的乐趣，康复旅程漫长。十字韧带撕裂需要手术治疗，我们提供手术和康复，使宠物重获运动能力。'
  },
  {
    id: '10',
    name: '癌症',
    content: '癌症的威胁或许会使宠物的未来变得不确定，需要大家的力量与支持共同面对。我们提供综合治疗，帮助宠物抵抗癌症，延长生存期。'
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

  .el-collapse-item__header {
  font-size: 16px; /* 您希望的字体大小 */
  color: rgb(93, 93, 93);
}


</style>