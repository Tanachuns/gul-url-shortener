import { type RouteConfig, index, route } from "@react-router/dev/routes";

export default [index("routes/home.tsx"),  route("/results", "routes/result.tsx"),] satisfies RouteConfig;
