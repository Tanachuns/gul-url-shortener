import type { Route } from "./+types/home";
import { StatsForm } from "~/components/StatsForm";
import StatsResult from "~/components/StatsResult";
import React, { useEffect } from "react";
import type { StatsResultData } from "~/types/StatusResultData";
import { useSearchParams } from "react-router";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Url Shortener" },
    { name: "description", content: "Welcome to  Shortener!" },
  ];
}

export default function Stats() {

      const [searchParams, setSearchParams] = useSearchParams();
      const _code = searchParams.get("code") || "";

  const [resultData, setData] = React.useState<StatsResultData | null>(null);

  useEffect(() => {
    if (_code) {
      fetchStats(_code);
    }
  }, []);

  const fetchStats = async (code: string) => {
    const url = `https://localhost:7219/api/links/${code.split("/").pop()}`;
    try {
      const response = await fetch(url, {
        method: "GET",
      });
      if (!response.ok) {
        console.error(response);
      }
      const result = await response.json();
      //navigate('/results', { state: { data: result } });
      setData(result.response);
    } catch (error: any) {
      console.error(error.message);
    }
  };

  const submitHander = async (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const formData = new FormData(event.currentTarget);
    if (!formData.get("shortUrl")) {
      alert("Shortened URL is required");
      return;
    }
    const code = formData.get("shortUrl")?.toString();
    if (!code) {
      alert("Shortened URL is required");
      return;
    }
    fetchStats(code);
  };

  const deleteHander = async () => {
    const code = resultData?.code || "";
    const url = `https://localhost:7219/api/links/${code}`;
    try {
      const response = await fetch(url, {
        method: "DELETE",
      });
       if (response.status !== 204) {
        console.error(response);
      }
      fetchStats(code);

    } catch (error: any) {
      console.error(error.message);
    }
  };

  const activateHandler = async () => {
    const code = resultData?.code || "";
    const url = `https://localhost:7219/api/links/${code}`;
    try {
      const response = await fetch(url, {
        method: "PATCH",
      });
      if (response.status !== 204) {
        console.error(response);
      }
      fetchStats(code);
    } catch (error: any) {
      console.error(error.message);
    }
  };
  return (
    <div className="w-full h-screen items-center justify-center">
      <div className="container mx-auto p-4 ">
        <h1 className="text-2xl font-bold">Your URL Stats</h1>
        <StatsForm submitHandler={submitHander} />
        <StatsResult
          resultData={resultData}
          deleteHandler={deleteHander}
          activateHandler={activateHandler}
        />
      </div>
    </div>
  );
}
