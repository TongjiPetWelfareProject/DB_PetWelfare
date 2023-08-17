<template>
  <div class="demo-collapse">
    <br><br>
    <el-text size="large" tag="b">康复故事</el-text>
    <br>
    <el-collapse v-model="activeName" max-height="200" accordion>
      <el-collapse-item v-for="story in recoveryStories" :key="story.id" :title="story.name" :name="story.id">
        <div>
          {{ story.content }}
        </div>
      </el-collapse-item>
    </el-collapse>
  </div>
  <el-pagination layout="prev, pager, next" :total="50" />
</template>

<script  setup>
import { ref, onMounted } from 'vue';
import medical_donate from '@/api/medical_donate';

const activeName = ref('1');
const recoveryStories = ref([]);

onMounted(async () => {
  try {
    const stories = await medical_donate.recoveryStoryAPI();
    recoveryStories.value = stories;
  } catch (error) {
    console.error('获取康复故事数据时出错：', error);
  }
});
</script>

  
  <style>
  .el-collapse{
    max-width:90%;
    max-height: 400px;
  }
</style>