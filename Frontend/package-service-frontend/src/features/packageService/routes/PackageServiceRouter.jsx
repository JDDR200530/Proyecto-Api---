import { Navigate, Route, Routes } from "react-router-dom"
import { NavBar } from "../components"
import {CreateOrder} from "../pages/CreateOrder"


export const PackageServiceRouter = () => {
  return (
    <div className="overflow-x-hidden bg-gray-100 w-screen h-screen bg-hero-pattern bg-no-repeat bg-cover" >
      <NavBar/>
      <div className="px-6 py-8">
            <div className="container flex justify-between mx-auto">
             <Routes>
                {/* <Route path= '/orders' element = {<HomePage/>}/> */}
                <Route path= '/createorder' element = {<CreateOrder/>}/>
                {/* <Route path= '/blog/post/:id' element = {<PostPage/>}/> */}
                <Route path= '/*' element = {<Navigate to = {"/home"}/> }/>
             </Routes>
            </div>
          </div>
          Footer
        </div>
  )
}
