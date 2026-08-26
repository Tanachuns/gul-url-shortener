import { useLocation } from "react-router";
import type { Route } from "./+types/home";
import { useRef, useState } from "react";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Url Shortener" },
    { name: "description", content: "Welcome to  Shortener!" },
  ];
}

export default function Result() {
  const location = useLocation();
  const { data } = location.state || {};

  const [isOpen, setIsOpen] = useState(false);
  const timeoutRef = useRef(null);

  const handleCopyClick = () => {
    if (data?.shortlink) {
      navigator.clipboard.writeText(data.shortlink);
      setIsOpen(true);
    }
  };
  return (
    <>
      <div className="container mx-auto p-4 ">
        <h1 className="text-2xl font-bold">Your shortened URL</h1>
        <p className="label text-sm">
          Copy the short link and share it in messages, texts, posts, websites
          and other locations.
        </p>
        <div className="">
          <div className="join mt-3 ">
            <input
              type="text"
              className="input join-item input-sm"
              value={data?.shortlink || ""}
            />

            <div
              className={` ${isOpen ? "tooltip-open tooltip tooltip-bottom" : ""}`}
              data-tip="URL Copied"
            >
              <button
                onClick={handleCopyClick}
                className="btn join-item  btn-sm"
              >
                Copy
              </button>
            </div>
          </div>
        </div>
      </div>
    </>
  );
}
