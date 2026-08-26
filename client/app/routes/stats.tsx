import type { Route } from "./+types/home";
import { ShortenerForm } from "../components/ShortenerForm";
import { useNavigate } from "react-router";
import { StatsForm } from "~/components/StatsForm";
import StatsResult from "~/components/StatsResult";
import React from "react";
import type { StatsResultData } from "~/types/StatusResultData";


export function meta({}: Route.MetaArgs) {
  return [
    { title: "Url Shortener" },
    { name: "description", content: "Welcome to  Shortener!" },
  ];
}

export default function Stats() {
    const [resultData, setData] = React.useState<StatsResultData | null>(null);

  const submitHander = async (event: React.FormEvent<HTMLFormElement>) => {

    event.preventDefault();

    const formData = new FormData(event.currentTarget);
  
    const code =  formData.get("shortUrl")?.toString() || "";
    
    const url = `https://localhost:7219/api/links/${code.split("/").pop()}`;
    try {
    const response = await fetch(url, {
      method: "GET"
    });
    if (!response.ok) {
      console.error(response);
    }
    const result = await response.json();
    //navigate('/results', { state: { data: result } });
    setData(result.response);
  } catch (error:any ) {
    console.error(error.message);
  }
  }
  return <div className="w-full h-screen items-center justify-center">
      <div className="container mx-auto p-4 ">
         <h1 className="text-2xl font-bold">Your URL Stats</h1>
        <StatsForm submitHandler={submitHander} />
        <StatsResult resultData={resultData} />
      </div>
  </div>;
}
