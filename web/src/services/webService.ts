
const baseUrl = 'https://localhost:5001/'

async function fetchData<T> (url : string): Promise<T> {
  const response = await fetch(`${baseUrl}${url}`);
  if (response.ok) {
    return await response.json() as T;
  }
  throw new Error(`fetch to ${url} failed, with ${response.status}:${response.statusText}`)
}

export default fetchData;
