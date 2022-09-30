<script>
  import { collection, query, orderBy, limit, onSnapshot, getFirestore } from "firebase/firestore";
  import { onDestroy, onMount } from "svelte";
  import { fly } from "svelte/transition";
  import ChatItem from "./ChatItem.svelte";

  const db = getFirestore();
  let list = null;
  let unsub = null;
  onMount(() => {
    const q = query(collection(db, "chat"), orderBy("creationDate", "desc"), limit(10));
    unsub = onSnapshot(q, (querySnapshot) => {
      const newList = [];
      querySnapshot.forEach((doc) => {
        newList.push({ ...doc.data(), id: doc.id });
        list = newList;
      });
    });
  });

  onDestroy(() => {
    if (unsub) unsub();
  });
</script>

{#if list}
  <div class="flex-1 flex flex-col-reverse gap-2 min-h-0 overflow-y-auto mb-2">
    {#each list as item (item.id)}
      <div transition:fly={{ x: -20, duration: 1000 }}>
        <ChatItem {item} />
      </div>
    {/each}
  </div>
{:else}
  <div class="flex-1 flex justify-center items-center opacity-50">불러오는 중입니다...</div>
{/if}
