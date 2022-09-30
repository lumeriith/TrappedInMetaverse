<script>
  export let item;
</script>

<div class="flex gap-4 py-2 px-5 bg-[#222]">
  <span class="opacity-50 font-bold font-serif">{item.author}</span>
  <span>{item.message}</span>
</div>

<style>
  div:hover {
    filter: brightness(1.2);
  }
</style>
