<script>
  import { app, isLoggedIn } from "../lib/firebase";
  import ChatBox from "../lib/components/ChatBox.svelte";
  import ChatList from "../lib/components/ChatList.svelte";
  import Header from "../lib/components/Header.svelte";
</script>

{#if $isLoggedIn}
  <div class="flex flex-col h-full">
    <Header />
    <ChatList />
    <ChatBox />
  </div>
{:else}
  <div class="flex justify-center opacity-50 h-full items-center text-4xl">익명 로그인 중입니다...</div>
{/if}

<style>
  :global(html, body, body > div) {
    background-color: black;
    color: white;
    min-height: 100% !important;
    height: 100vh;
  }
</style>
