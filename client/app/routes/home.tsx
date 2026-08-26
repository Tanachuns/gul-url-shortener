import type { Route } from "./+types/home";
import { ShortenerForm } from "../layouts/ShortenerForm";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Url Shortener" },
    { name: "description", content: "Welcome to  Shortener!" },
  ];
}

export default function Home() {
  return <>
  <ShortenerForm/>
  </>;
}
