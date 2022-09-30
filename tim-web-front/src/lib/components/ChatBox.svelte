<script>
  import { doc, collection, setDoc, getFirestore, serverTimestamp } from "firebase/firestore";
  import { userName } from "../firebase";

  let content = "";

  let isBusy = false;
  $: isValid = !!content.trim();

  let db = getFirestore();

  async function sendMessage(e) {
    e.preventDefault();
    if (isBusy) return;
    if (!isValid) return;
    isBusy = true;
    try {
      await setDoc(doc(collection(db, "chat")), {
        author: userName,
        creationDate: serverTimestamp(),
        message: content,
      });
      content = "";
    } catch (err) {
      console.error(err);
    }
    isBusy = false;
  }
</script>

<form on:submit={sendMessage}>
  <input type="text" class="bg-[#111] focus:bg-[#333] disabled:opacity-50 outline-none p-5 w-full" placeholder="메시지를 입력하세요" bind:value={content} disabled={isBusy} />
</form>
