import type { Route } from "./+types/home";
import { ShortenerForm } from "../layouts/ShortenerForm";
import { useNavigate } from "react-router";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "Url Shortener" },
    { name: "description", content: "Welcome to  Shortener!" },
  ];
}

export default function Home() {
    const navigate = useNavigate();

  const submitHander = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData = new FormData(event.currentTarget);
    const requestBody = {
      Url: formData.get("Longurl"),
      customAlias: formData.get("customAlias"),
      androidUrl: formData.get("androidUrl"),
      iosUrl: formData.get("iosUrl"),
    };
    const url = "https://localhost:7219/api/links";
    try {
    const response = await fetch(url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json"
      },
      body: JSON.stringify(requestBody)
    });
    if (!response.ok) {
      console.error(response);
    }
    const result = await response.json();
    navigate('/results', { state: { data: result } });
    console.log(result);
  } catch (error:any ) {
    console.error(error.message);
  }
  }
  return <div className="w-full h-screen items-center justify-center">
      <ShortenerForm submitHandler={submitHander} />
  </div>;
}
