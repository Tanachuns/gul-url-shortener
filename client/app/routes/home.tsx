import type { Route } from "./+types/home";
import { ShortenerForm } from "../components/ShortenerForm";
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
    if (!formData.get("Longurl")) {
      alert("Url is required");
      return;
    }
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
          "Content-Type": "application/json",
        },
        body: JSON.stringify(requestBody),
      });
      if (!response.ok ) {
        console.error(response);
        return;
      }
      const result = await response.json();
      navigate("/results", { state: { data: result } });
      console.log(result);
    } catch (error: any) {
      console.error(error.message);
    }
  };
  return (
    <div className="w-full h-screen items-center justify-center">
      <div className="container mx-auto p-4 ">
        <h1 className="text-2xl font-bold">Shorten your URL</h1>
        <ShortenerForm submitHandler={submitHander} />
      </div>
    </div>
  );
}
